using System;
using System.Text;
using Afterman.nRepo.NHibernate;
using Afterman.nRepo.Samples.Usage;

namespace Afterman.nRepo.Samples
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var config = new nRepoConfiguration().With("Default",
                new NHibernateConfiguration()
                    .AddMappings(typeof(Program).Assembly)
                    .ConnectionString("data source=.;initial catalog=samples;integrated security=sspi;")
                    .ShowSql(true)
                    .UpdateSchemaOnDebug()
                );
            config.Start();
            using (var unitOfWork = config.GetMasterUnitOfWork())
            {
                while (true)
                {
                    Console.WriteLine("Press r to test a rollback. Press s to test a save.");

                    try
                    {
                        unitOfWork.Begin();
                        var repo = new SampleRepository(unitOfWork);
                        var tooLongField = new StringBuilder();
                        var justRightField = Guid.NewGuid().ToString();
                        for (var i = 0; i < 100; i++)
                        {
                            tooLongField.Append(Guid.NewGuid());
                        }
                        repo.Add(new Sample()
                        {
                            SampleField = Console.ReadLine() == "r" ? tooLongField.ToString() : justRightField,
                        });
                        unitOfWork.End();
                        Console.WriteLine($"Committed with value {justRightField}");
                    }
                    catch (Exception e)
                    {
                        unitOfWork.End(e);
                        Console.WriteLine("rolled back transaction");
                    }
                }

            }
        }
    }
}
