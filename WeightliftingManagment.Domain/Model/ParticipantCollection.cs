using WeightliftingManagment.MvvmSupport.Collections;

namespace WeightliftingManagment.Domain.Model
{
    public class ParticipantCollection : FullyObservableCollection<Participant>
    {
        public ParticipantCollection() : base()
        {
        }
        public ParticipantCollection(IEnumerable<Participant> enumerable) : base(enumerable)
        {

        }
        public ParticipantCollection(List<Participant> enumerable) : base(enumerable)
        {

        }

        public void AddNewParticipant(Participant participant)
        {
            if (Contains(participant)) throw new ArgumentException("Participant egsist in collection");

            Add(participant);
        }

        public Participant GetParticipantByID(Guid participantId) => this.Single(item => item.ParticipantId == participantId);

        public Participant GetParticipantWithComesUp() => this.Single(item => item.ComesUp);

        public Participant GetParticipantWithNext() => this.Single(item => item.Next);

        public bool IsSnatch() => this.Select(x => x.IsSnatchDeclared()).Any();

        public void SortByGrupaThenByNrStart()
        {
            var list = this.ToList().OrderBy(x => x.Group).ThenBy(x => x.StartNumber).ToList();
            Clear();
            list.ForEach(item => Add(item));
        }

        public void SortByNrStart()
        {
            var list = this.ToList().OrderBy(x => x.StartNumber).ToList();
            Clear();
            list.ForEach(item => Add(item));
        }

        public void SortByGrupa()
        {
            var list = this.ToList().OrderBy(x => x.Group).ToList();
            Clear();
            list.ForEach(item => Add(item));
        }

        public void SortByNrStartThenByGrupa()
        {
            var list = this.ToList().OrderBy(x => x.StartNumber).ThenBy(x => x.Group).ToList();
            Clear();
            list.ForEach(item => Add(item));
        }

        private void SetAttemptStatusForParticipantId(Guid participantId, int attemptNumber, AttemptStatus attemptStatus, bool isRwanie) => GetParticipantByID(participantId).SetAttempt(attemptNumber, attemptStatus, isRwanie);

        public void SetAttemptStatusToGoodLiftForParticipantId(Guid participantId, int attemptNumber, bool isRwanie)
            => SetAttemptStatusForParticipantId(participantId, attemptNumber, AttemptStatus.GoodLift, isRwanie);

        public void SetAttemptStatusToNoLiftForParticipantId(Guid participantId, int attemptNumber, bool isRwanie)
             => SetAttemptStatusForParticipantId(participantId, attemptNumber, AttemptStatus.NoLift, isRwanie);
    }
}
