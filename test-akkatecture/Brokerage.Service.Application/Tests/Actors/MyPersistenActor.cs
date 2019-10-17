using Akka.Actor;
using Akka.Persistence;
using System;
using System.Collections.Generic;

namespace Brokerage.Service.Application.Tests.Actors
{
    public class MyPersistentActor : ReceivePersistentActor
    {
        private int _msgsSinceLastSnapshot;

        private List<string> _messages = new List<string>();

        public class GetMessages { }

        public override string PersistenceId => "testactor-id";

        public MyPersistentActor()
        {
            Recover<string>(i => _messages.Add(i));

            Recover<SnapshotOffer>(i =>
            {
                if (i.Snapshot is List<string> messages) _messages.AddRange(messages);
            });

            Command<string>(i => Persist(i, s =>
            {
                _messages.Add(i);
                if (++_msgsSinceLastSnapshot >= 10)
                {
                    SaveSnapshot(_messages);
                    _msgsSinceLastSnapshot = 0;
                }
            }));

            Command<SaveSnapshotSuccess>(i =>
            {
                DeleteMessages(i.Metadata.SequenceNr);
                Console.WriteLine($"Saving snapshot succeeded! to save snapshot: {i.Metadata.SequenceNr}");
            });

            Command<SaveSnapshotFailure>(i => Console.WriteLine($"Failed to save snapshot: {i.Cause.Message}"));

            Command<GetMessages>(i =>
            {
                IReadOnlyList<string> list = new List<string>(_messages);

                Sender.Tell(list);
            });
        }
    }
}
