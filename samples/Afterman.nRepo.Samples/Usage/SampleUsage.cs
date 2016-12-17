using FluentNHibernate.Mapping;

namespace Afterman.nRepo.Samples.Usage
{
   

    public class Sample
    {
        public virtual int Id { get; set; }
        public virtual string SampleField { get; set; }
    }

    public class SampleMap : 
        ClassMap<Sample>
    {
        public SampleMap()
        {
            Table("Sample");
            Id(x => x.Id).GeneratedBy.Identity().Default(0);
            Map(x => x.SampleField);
        }
    }

    public class SampleRepository : 
        RepositoryBase<Sample>
        , IRepository<Sample>
    {
        public SampleRepository(IMasterUnitOfWork unitOfWorkFactory) :
            base(unitOfWorkFactory, "Default")
        {
            
        }
    }
}
