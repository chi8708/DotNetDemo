using EFCodeFirst.Entity;
using EFCodeFirst.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst
{
   public class ShopContext:DbContext
   {
       public ShopContext(string nameOrConnectionString="")
            : base(nameOrConnectionString)
        {
           
        }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           //dynamically load all configuration
           //System.Type configType = typeof(LanguageMap);   //any of your configuration classes here
           //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()

           var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
               type.BaseType.GetGenericTypeDefinition() == typeof(NopEntityTypeConfiguration<>));
           foreach (var type in typesToRegister)
           {
               dynamic configurationInstance = Activator.CreateInstance(type);
               modelBuilder.Configurations.Add(configurationInstance);
           }
           //...or do it manually below. For example,
           //modelBuilder.Configurations.Add(new LanguageMap());


           base.OnModelCreating(modelBuilder);
       }

       /// <summary>
       /// Get DbSet
       /// </summary>
       /// <typeparam name="TEntity">Entity type</typeparam>
       /// <returns>DbSet</returns>
       public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
       {
           return base.Set<TEntity>();
       }

   }
}
