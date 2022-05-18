namespace ContractManagement.Domain.Aggregates.ContractAggregate
{
    public partial class Contract
    {
        public ValueTask ChangeContractTerm(ChangeContractTerm command)
        {
            EnsureNotCancelled();
            EnsureValidTerm(command.StartDate, command.EndDate);
            if (IsValid)
            {
                var contractTermChanged = ContractTermChanged.CreateFrom(command);
                ApplyDomainEvent(contractTermChanged);
            }
            return ValueTask.CompletedTask;
        }

        private void Handle(ContractTermChanged domainEvent)
        {
            ContractTerm = Duration.Parse(domainEvent.StartDate, domainEvent.EndDate);
        }
    }
}