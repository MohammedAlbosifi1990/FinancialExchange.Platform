using Shared.Core.Domain.Enum;

namespace Shared.Core.Domain.Entities;

public class Transaction : IBaseEntity
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }
    public TransactionState State { get; set; }
    public decimal Fee { get; set; }
    public DateTime DateTime { get; set; }
    public required string Reference { get; set; } 
        
    
    public required string CustomerFromName { get; set; } 
    public required string CustomerFromPhone { get; set; } 
    
    public required string CustomerToName { get; set; } 
    public required string CustomerToPhone { get; set; } 
    
    public string? Note { get; set; } 
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid OfficeFromId { get; set; }
    public virtual Office OfficeFrom { get; set; } = null!;
    
    public Guid OfficeToId { get; set; }
    public virtual Office OfficeTo { get; set; } = null!;
    
    
    // [ForeignKey("FalseWorkflowStepId")]
    // public virtual AccountOpenerWorkflowStep FalseWorkflowStep { get; set; }
}