using Dev.Tools.Exceptions;
using PostSharp.Aspects.Dependencies;
using PostSharp.Extensibility;
using System;

namespace Sinba.BusinessLogic
{
    /// <summary>
    /// Attribut de gestion des exception pour les services.
    /// Pratique la substitution de retour des méthode publiques en remplaçant en cas d'exception le DTO avec un DTO contenant les erreurs des exceptions récupérées
    /// </summary>
    [Serializable]
    [ProvideAspectRole(StandardRoles.ExceptionHandling)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.Tracing)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Public)]
    public class SinbaServiceExceptionHandlerAttribute : ServicePublicExceptionHandlerAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinbaServiceExceptionHandlerAttribute"/> class.
        /// Détermine que la méthode de la classe sur laquelle est aposé l'attribut qui sera appelée lors qu'une exception s'appelle ExceptionHandlerMethod
        /// Définir dans SinbaServiceBase une surcharge de cette méthode si un comportement spécifique doit être apporter à cette méthode.
        /// </summary>
        public SinbaServiceExceptionHandlerAttribute()
            : base("ExceptionHandlerMethod")
        {

        }
    }
}
