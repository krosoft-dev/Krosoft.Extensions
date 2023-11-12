using Krosoft.Extensions.Core.Models.Business;

namespace Krosoft.Extensions.Identity.Abstractions.Interfaces;

public interface IKrosoftTokenBuilderService
{
    KrosoftToken Build();
}