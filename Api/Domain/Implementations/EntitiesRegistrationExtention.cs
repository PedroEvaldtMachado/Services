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
                a.HasMany<CountryObligations>(c => c.CountryObligations)
                    .WithOne()
                    .HasForeignKey(c => c.CountryId);

                a.Navigation(c => c.CountryObligations).AutoInclude();
            });

            modelBuilder.Entity<Person>(a =>
            {
                a.HasMany<PersonDetail>(c => c.PersonDetails)
                    .WithOne()
                    .HasForeignKey(c => c.PersonId);

                a.HasOne<Contractee>()
                    .WithOne(c => c.Person)
                    .IsRequired()
                    .HasForeignKey<Contractee>(c => c.PersonId);

                a.HasOne<Contractor>()
                    .WithOne(c => c.Person)
                    .IsRequired()
                    .HasForeignKey<Contractor>(c => c.PersonId);

                a.Navigation(c => c.PersonDetails).AutoInclude();
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

                a.Navigation(c => c.Person).AutoInclude();
                a.Navigation(c => c.Schedulings).AutoInclude();
                a.Navigation(c => c.SchedulingDates).AutoInclude();
            });

            modelBuilder.Entity<Contractor>(a =>
            {
                a.HasMany<Contract>()
                    .WithOne()
                    .HasForeignKey(c => c.ContractorId);

                a.Navigation(c => c.Person).AutoInclude();
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
                a.HasMany<Scheduling>(c => c.Schedulings)
                    .WithOne()
                    .HasForeignKey(c => c.ContractId);

                a.Navigation(c => c.ContractDetails).AutoInclude();
                a.Navigation(c => c.Schedulings).AutoInclude();
            });
        }
    }
}
