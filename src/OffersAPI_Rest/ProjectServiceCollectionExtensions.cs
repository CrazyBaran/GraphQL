using Datasource;
using Datasource.Repositories;

namespace OffersAPI_Rest
{
    using OffersAPI_Rest.Commands;
    using OffersAPI_Rest.Mappers;
    using OffersAPI_Rest.ViewModels;
    using Boxed.Mapping;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services
                .AddSingleton<IGetOfferCommand, GetOfferCommand>()
                .AddSingleton<IGetOffersCommand, GetOffersCommand>()
                .AddSingleton<IPostOfferCommand, PostOfferCommand>()
                .AddSingleton<IPostOfferApplicationCommand, PostOfferApplicationCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IMapper<Datasource.OfferData, Offer>, OfferDataToOfferMapper>()
                .AddSingleton<IMapper<Datasource.OfferApplicationData, OfferApplicationData>, OfferApplicationToOfferApplicationMapper>()
                .AddSingleton<IMapper<SaveOffer, Datasource.OfferData>, OfferDataToSaveOfferMapper>()
                .AddSingleton<IMapper<SaveOfferApplication, Datasource.OfferApplicationData>, OfferToSaveOfferApplicationMapper>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<IOfferRepository, OfferRepository>();
    }
}
