using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WebAPIExample.Models
{
   
    public partial class StockItem
    {
        public StockItem()
        {
               
        }
        public StockItem(int id)
        {
            StockItemID = id;
        }
        public int StockItemID { get; set; }

        public string StockItemName { get; set; }

        public int? SupplierID { get; set; }

        public int? ColorID { get; set; }

        public int UnitPackageID { get; set; }

        public int OuterPackageID { get; set; }

        public string Brand { get; set; }

        public string Size { get; set; }

        public int LeadTimeDays { get; set; }
    
        public int QuantityPerOuter { get; set; }

        public bool IsChillerStock { get; set; }

        public string Barcode { get; set; }

        public decimal TaxRate { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal RecommendedRetailPrice { get; set; }

        public decimal TypicalWeightPerUnit { get; set; }

        public string MarketingComments { get; set; }

        public string InternalComments { get; set; }

        public string CustomFields { get; set; }

        public string Tags { get; set; }

        public string SearchDetails { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }


    //DBde yukaridaki sinifin elemanlarini nasil bir duzende kaydedecegini gosterir. 
    public class StockItemConfiguration:IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            //eger yanlis anlamadı isem ilk Stock kismi benim yukarıda sinifta belirttigim tablo
            //example ise semam.
            builder.ToTable("StockItems", "Example");

            //haskey metodum benim hangisini primary yapacagimi belirttigim kisim.
            builder.HasKey(p=>p.StockItemID);

            //asagida diger degerlere hangi ozellikleri alacagini belirtmek icin property metodu ile beraber
            //sutunun tipi icin ColumnType metodu ve en son olarak
            //null deger alip almamasi icin ise isrequired metodu.

            builder.Property(p=>p.Barcode).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.Brand).HasColumnType("nvarchar(100)");
            builder.Property(p => p.ColorID).HasColumnType("int");
            builder.Property(p => p.CustomFields).HasColumnType("nvarchar(MAX)");
            builder.Property(p => p.InternalComments).HasColumnType("nvarchar(MAX)");
            builder.Property(p => p.IsChillerStock).HasColumnType("bit").IsRequired();
            builder.Property(p => p.LastEditedBy).HasColumnType("int").IsRequired();
            builder.Property(p => p.LeadTimeDays).HasColumnType("int").IsRequired();
            builder.Property(p => p.MarketingComments).HasColumnType("nvarchar(MAX)");
            builder.Property(p => p.OuterPackageID).HasColumnType("int").IsRequired();
            builder.Property(p => p.QuantityPerOuter).HasColumnType("int").IsRequired();
            builder.Property(p => p.RecommendedRetailPrice).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.Size).HasColumnType("nvarchar(40)");
            builder.Property(p => p.StockItemName).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.SupplierID).HasColumnType("int").IsRequired();
            builder.Property(p => p.TaxRate).HasColumnType("decimal(18, 3)").IsRequired();
            builder.Property(p => p.UnitPrice).HasColumnType("decimal(18, 3)").IsRequired();
            builder.Property(p => p.UnitPackageID).HasColumnType("int").IsRequired();
            builder.Property(p => p.TypicalWeightPerUnit).HasColumnType("decimal(18, 3)").IsRequired();


            builder.Property(p => p.StockItemID).HasColumnName("int").IsRequired().HasDefaultValueSql("NEXT VALUE FOR [Sequences].[StockItemID]");
            builder.Property(p => p.Tags).HasColumnName("nvarchar(Max)").IsRequired().HasComputedColumnSql("json_query([CustomFields],N'$.Tags')");
            builder.Property(p => p.SearchDetails).HasColumnName("int").IsRequired().HasComputedColumnSql("concat([StockItemName],N' ',[MarketingComments])");
            builder.Property(p => p.ValidFrom).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(p => p.ValidTo).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAddOrUpdate();

        }
    }




    //DBContext Kismi
    public class WebAPIContext : DbContext
    {
        //Constructor
        public WebAPIContext(DbContextOptions<WebAPIContext> options):base(options)
        {
        }


        //Model olusturma kismi.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new StockItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        //DBSet
        public DbSet<StockItem> StockItems { get; set; }
    }

     

}
