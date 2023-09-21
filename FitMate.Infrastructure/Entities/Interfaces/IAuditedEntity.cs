namespace FitMate.Infrastructure.Entities.Interfaces;

public interface IAuditedEntity
{
    DateTime CreatedAt { get; set; }
}