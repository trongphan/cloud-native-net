namespace ContractManagement.Domain.Aggregates.ContractAggregate
{
    public partial class Contract
    {
        public ValueTask CancelContract(CancelContract command)
        {
            CheckBusinessRules();
            if (!IsValid)
            {
                return ValueTask.CompletedTask;
            }

            var contractCancelled = ContractCancelled.CreateFrom(command);

            ApplyDomainEvent(contractCancelled);
            
            return ValueTask.CompletedTask;
        }

        private void Handle(ContractCancelled domainEvent)
        {
            Cancelled = true;
        }

        private void CheckBusinessRules()
        {
            EnsureNotCancelled();
            
            if (DateTime.Now.Date >= ContractTerm?.EndDate.Date.AddYears(-3))
            {
                AddBusinessRuleViolation("Contract can not be cancelled if it is within 3 years from the end of its term.");
            }            
        }
    }
}