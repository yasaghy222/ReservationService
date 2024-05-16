using System.Reflection;
using ReservationService.DTOs;
using ReservationService.Entities;
using ReservationService.Models;
using Mapster;
using ReservationService.Shared;
namespace ReservationService.Mappings;

public static class MapsterConfig
{
	public static void RegisterMapsterConfiguration(this IServiceCollection services)
	{
		TypeAdapterConfig<AddReservationDto, Reservation>.NewConfig()
		.Map(dto => dto.CustomerJson, s => s.Customer.ToJson())
		.Map(dto => dto.ProviderJson, s => s.Provider.ToJson())
		.Map(dto => dto.PricePlanItemJson, s => "");

		TypeAdapterConfig<Reservation, ReservationDetail>.NewConfig()
		.Map(dto => dto.Customer, s => s.CustomerJson.FromJson<Customer>())
		.Map(dto => dto.Provider, s => s.ProviderJson.FromJson<Provider>());

		TypeAdapterConfig<Reservation, ReservationInfo>.NewConfig()
		.Map(dto => dto.CustomerFullName, s => s.CustomerJson.FromJson<Provider>().FullName);
	}
}
