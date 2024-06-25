using Api.Domain.Entities.Contracts;
using Api.Domain.Entities.Countrys;
using Api.Domain.Entities.Persons;
using Api.Domain.Entities.Schedulings;
using Api.Domain.Entities.Services;
using Api.Domain.Entities.Stakeholders;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Implementations
{
    public static class EntitiesRegistrationExtention
    {
        public static void AddEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(a =>
            {
                a.Navigation(c => c.CountryObligations).AutoInclude();

                a.HasMany<CountryObligations>(c => c.CountryObligations)
                    .WithOne()
                    .HasForeignKey(c => c.CountryId);
            });

            modelBuilder.Entity<Person>(a =>
            {
                a.Navigation(c => c.PersonDetails).AutoInclude();

                a.HasMany<PersonDetail>(c => c.PersonDetails)
                    .WithOne()
                    .HasForeignKey(c => c.PersonId);

                a.HasOne<Contractee>()
                    .WithOne()
                    .IsRequired(false)
                    .HasForeignKey<Contractee>(c => c.PersonId);

                a.HasOne<Contractor>()
                    .WithOne()
                    .IsRequired(false)
                    .HasForeignKey<Contractor>(c => c.PersonId);

            });

            modelBuilder.Entity<Contractee>(a =>
            {
                a.HasMany<Contract>()
                    .WithOne()
                    .HasForeignKey(c => c.ContracteeId);

                a.HasMany<Scheduling>(c => c.Schedulings)
                    .WithOne()
                    .HasForeignKey(c => c.ContracteeId);

                a.HasMany<SchedulingDate>(c => c.SchedulingDates)
                    .WithOne()
                    .HasForeignKey(c => c.ContracteeId);
            });

            modelBuilder.Entity<Contractor>(a =>
            {
                a.HasMany<Contract>()
                    .WithOne()
                    .HasForeignKey(c => c.ContractorId);
            });

            modelBuilder.Entity<ServiceType>(a =>
            {
                a.HasMany<ContracteeServiceProvide>()
                    .WithOne()
                    .HasForeignKey(c => c.ServiceTypeId);
            });

            modelBuilder.Entity<ContracteeServiceProvide>(a =>
            {
                a.HasMany<ContracteeServiceDetail>(c => c.ContracteeServiceDetails)
                    .WithOne()
                    .HasForeignKey(c => c.ContracteeServiceProvideId);
            });

            modelBuilder.Entity<Contract>(a =>
            {
                a.Navigation(c => c.ContractDetails).AutoInclude();
                a.Navigation(c => c.Schedulings).AutoInclude();

                a.HasMany<Scheduling>(c => c.Schedulings)
                    .WithOne()
                    .HasForeignKey(c => c.ContractId);
            });
        }
    }
}
