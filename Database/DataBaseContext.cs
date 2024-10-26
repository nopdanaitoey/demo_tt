using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.Trucks;
using demo_tt.Entities;
using demo_tt.Entities.Common;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace demo_tt.Database
{
    public class DataBaseContext : DbContext
    {
        private readonly ICacheRepository _cacheRepository;
        public DataBaseContext(DbContextOptions<DataBaseContext> options, ICacheRepository cacheRepository) : base(options)
        {
            _cacheRepository = cacheRepository;
        }
        public DbSet<AffectedAreas> AffectedAreas { get; set; }
        public DbSet<ResourceTrucks> ResourceTrucks { get; set; }
        public DbSet<MasterResource> MasterResources { get; set; }
        public DbSet<ResourceAffectedAreas> ResourceAffectedAreas { get; set; }
        public DbSet<TravelTimeToAreas> TravelTimeToAreas { get; set; }
        public DbSet<Trucks> Trucks { get; set; }
        public DbSet<Assignments> Assignments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdatedTime = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.Id == Guid.Empty) entry.Entity.Id = Guid.NewGuid();
                    entry.Entity.IsActive = ConstantsEntity.ACTIVE;
                    entry.Entity.CreatedTime = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        public async Task SeedDatabase()
        {
            DateTime now = DateTime.Now;

            TravelTimeToAreas.RemoveRange(TravelTimeToAreas);
            ResourceTrucks.RemoveRange(ResourceTrucks);
            ResourceAffectedAreas.RemoveRange(ResourceAffectedAreas);
            MasterResources.RemoveRange(MasterResources);
            Trucks.RemoveRange(Trucks);
            AffectedAreas.RemoveRange(AffectedAreas);
            Assignments.RemoveRange(Assignments);

            await _cacheRepository.RemoveData(ConstantsEntity.REDIS_MASTER_RESOURCE);
            await _cacheRepository.RemoveData(ConstantsEntity.REDIS_ASSIGNMENTS);
            await _cacheRepository.RemoveData(ConstantsEntity.REDIS_TRUCKS);
            await _cacheRepository.RemoveData(ConstantsEntity.REDIS_AFFECTED_AREAS);
            // await Database.MigrateAsync();
       


            Guid MedicineId = Guid.Parse("125148ec-6566-1234-85e8-09a224712345");
            Guid FoodId = Guid.Parse("019f436b-dda2-4150-94c1-614b042ded2f");
            Guid WaterId = Guid.Parse("00546f6e-f5ea-4e51-965a-9f9054a7b4d5");

            MasterResources.AddRange(new List<MasterResource>
            {
                new MasterResource
                {
                    Id = MedicineId,
                    Name = "Medicine",
                    IsActive = ConstantsEntity.ACTIVE
                },
                new MasterResource
                {
                    Id = FoodId,
                    Name = "Food",
                    IsActive = ConstantsEntity.ACTIVE
                },
                new MasterResource
                {
                    Id = WaterId,
                    Name = "Water",
                    IsActive = ConstantsEntity.ACTIVE
                }
            });

            Guid A1Id = Guid.Parse("0d03251e-60f3-43e1-8ff8-cf5b8fc6b2b8");
            Guid A2Id = Guid.Parse("b487e3a1-4f50-452e-9c03-0274525c4f37");
            AffectedAreas.AddRange(new List<Entities.AffectedAreas>
            {
                new AffectedAreas
                {
                    Id = A1Id,
                    AreaID = "A1",
                    UrgencyLevel = 5,
                    TimeConstraint = 6,

                },
                  new AffectedAreas
                {
                    Id = A2Id,
                    AreaID = "A2",
                    UrgencyLevel = 4,
                    TimeConstraint = 4,

                }
            });



            Guid TruckT1Id = Guid.Parse("9cb786cf-723f-480f-9352-c257900f99a8");
            Guid TruckT2Id = Guid.Parse("b11705e5-8def-42ea-9abd-df9ac95c64b5");
            Trucks.AddRange(new List<Trucks>{
                new Trucks
                {
                    Id = TruckT1Id,
                    TruckID = "T1",
                    IsActive = ConstantsEntity.ACTIVE,

                },
                new Trucks
                {
                    Id = TruckT2Id,
                    TruckID = "T2",
                    IsActive = ConstantsEntity.ACTIVE,

                }
            });
            var _truck = Trucks.ToList();


            await SaveChangesAsync();
            ResourceAffectedAreas.AddRange(new List<ResourceAffectedAreas>{
                    new ResourceAffectedAreas
                        {
                            Id = Guid.NewGuid(),
                            AreaID = A2Id,
                            ResourceID = MedicineId,
                            Quantity = 50,
                            IsActive = ConstantsEntity.ACTIVE
                        },
                         new ResourceAffectedAreas
                        {
                            Id = Guid.NewGuid(),
                            AreaID = A1Id,
                            ResourceID = FoodId,
                            Quantity = 200,
                            IsActive = ConstantsEntity.ACTIVE
                        },
                        new ResourceAffectedAreas
                        {
                            Id = Guid.NewGuid(),
                            AreaID = A1Id,
                            ResourceID = WaterId,
                            Quantity = 300,
                            IsActive = ConstantsEntity.ACTIVE
                        }
            });

            var _addResourceTrucks = new List<ResourceTrucks>{
                    new ResourceTrucks
                        {
                            Id = Guid.NewGuid(),
                            ResourceID = MedicineId,
                            Quantity = 60,
                            TruckID = TruckT2Id,
                            AvailableQuantity = 60,
                            IsActive = ConstantsEntity.ACTIVE
                        }  ,
                        new ResourceTrucks
                        {
                            Id = Guid.NewGuid(),
                            TruckID = TruckT1Id,
                            ResourceID = FoodId,
                            Quantity = 250,
                            AvailableQuantity = 250,
                            IsActive = ConstantsEntity.ACTIVE
                        },
                        new ResourceTrucks
                        {
                            Id = Guid.NewGuid(),
                            ResourceID = WaterId,
                            TruckID = TruckT1Id,
                            Quantity = 300,
                            AvailableQuantity = 300,
                            IsActive = ConstantsEntity.ACTIVE
                        }
            };
            foreach (var item in _addResourceTrucks)
            {
                ResourceTrucks.Add(item);
                _truck.Where(x => x.Id == item.TruckID).FirstOrDefault().ResourceTrucks.Add(item);

            }

            var _truckAreas = new List<TravelTimeToAreas>{
                new TravelTimeToAreas
                        {
                            Id = Guid.NewGuid(),
                            TruckID = TruckT1Id,
                            AreaID = A1Id,
                            TravelTime = 5,
                            IsActive = ConstantsEntity.ACTIVE
                        },
                new TravelTimeToAreas
                        {
                            Id = Guid.NewGuid(),
                            TruckID = TruckT1Id,
                            AreaID = A2Id,
                            TravelTime = 3,
                            IsActive = ConstantsEntity.ACTIVE
                        },
                        new TravelTimeToAreas
                        {
                            Id = Guid.NewGuid(),
                            TruckID = TruckT2Id,
                            AreaID = A1Id,
                            TravelTime = 4,
                            IsActive = ConstantsEntity.ACTIVE
                        },
                        new TravelTimeToAreas
                        {
                            Id = Guid.NewGuid(),
                            TruckID = TruckT2Id,
                            AreaID = A2Id,
                            TravelTime = 2,
                            IsActive = ConstantsEntity.ACTIVE
                        }
            };
            foreach (var item in _truckAreas)
            {
                TravelTimeToAreas.AddRange(item);
                _truck.Where(x => x.Id == item.TruckID).FirstOrDefault().TravelTimeToAreas.Add(item);

            }


            await _cacheRepository.SetCacheData<List<Trucks>>(ConstantsEntity.REDIS_TRUCKS, _truck, DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));
            await _cacheRepository.SetCacheData<List<AffectedAreas>>(ConstantsEntity.REDIS_AFFECTED_AREAS, AffectedAreas.ToList(), DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));
            await _cacheRepository.SetCacheData<List<MasterResource>>(ConstantsEntity.REDIS_MASTER_RESOURCE, MasterResources.ToList(), DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));
            await SaveChangesAsync();
        }

    }
}