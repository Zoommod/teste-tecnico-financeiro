using System;

namespace Domain.Exceptions;

public class EntityNotFoundException : DomainException
{
    public string EntityName { get; }
    public Guid EntityId { get; }

    public EntityNotFoundException(string entityName, Guid entityId) : base($"{entityName} com Id '{entityId}' não foi encontrado(a).")
    {
        EntityName = entityName;
        EntityId = entityId;
    }
}
