using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace diet_server_api.Models
{
    public partial class mdzcojxmContext : DbContext
    {
        public mdzcojxmContext()
        {
        }

        public mdzcojxmContext(DbContextOptions<mdzcojxmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<Dietsuppliment> Dietsuppliments { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<DiseasePatient> DiseasePatients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Foodinput> Foodinputs { get; set; }
        public virtual DbSet<Individualrecipe> Individualrecipes { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Mealsbeforediet> Mealsbeforediets { get; set; }
        public virtual DbSet<Mealtake> Mealtakes { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PgStatStatement> PgStatStatements { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductParameter> ProductParameters { get; set; }
        public virtual DbSet<Productdiet> Productdiets { get; set; }
        public virtual DbSet<Questionary> Questionaries { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Supplement> Supplements { get; set; }
        public virtual DbSet<TempUser> TempUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=tai.db.elephantsql.com;Database=mdzcojxm;Username=mdzcojxm;Password=Ko_V9TI2V1PH2XI3DC1VYYt2pBTqRopP");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("btree_gin")
                .HasPostgresExtension("btree_gist")
                .HasPostgresExtension("citext")
                .HasPostgresExtension("cube")
                .HasPostgresExtension("dblink")
                .HasPostgresExtension("dict_int")
                .HasPostgresExtension("dict_xsyn")
                .HasPostgresExtension("earthdistance")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("hstore")
                .HasPostgresExtension("intarray")
                .HasPostgresExtension("ltree")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("pgrowlocks")
                .HasPostgresExtension("pgstattuple")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Day>(entity =>
            {
                entity.HasKey(e => e.Idday)
                    .HasName("day_pk");

                entity.ToTable("day");

                entity.Property(e => e.Idday).HasColumnName("idday");

                entity.Property(e => e.Daynumber).HasColumnName("daynumber");

                entity.Property(e => e.DietIddiet).HasColumnName("diet_iddiet");

                entity.HasOne(d => d.DietIddietNavigation)
                    .WithMany(p => p.Days)
                    .HasForeignKey(d => d.DietIddiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("day_diet");
            });

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.HasKey(e => e.Iddiet)
                    .HasName("diet_pk");

                entity.ToTable("diet");

                entity.Property(e => e.Iddiet).HasColumnName("iddiet");

                entity.Property(e => e.Carbs).HasColumnName("carbs");

                entity.Property(e => e.Datefrom)
                    .HasColumnType("date")
                    .HasColumnName("datefrom");

                entity.Property(e => e.Dateto)
                    .HasColumnType("date")
                    .HasColumnName("dateto");

                entity.Property(e => e.Datetoinformaboutchanges)
                    .HasColumnType("date")
                    .HasColumnName("datetoinformaboutchanges");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(15000)
                    .HasColumnName("description");

                entity.Property(e => e.Fat).HasColumnName("fat");

                entity.Property(e => e.Fiber).HasColumnName("fiber");

                entity.Property(e => e.Idpatient).HasColumnName("idpatient");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Numberofmealsperday).HasColumnName("numberofmealsperday");

                entity.Property(e => e.Protein).HasColumnName("protein");

                entity.Property(e => e.Totalamountofcalories)
                    .HasPrecision(10, 2)
                    .HasColumnName("totalamountofcalories");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Diets)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("diet_patient");
            });

            modelBuilder.Entity<Dietsuppliment>(entity =>
            {
                entity.HasKey(e => e.Iddietsuppliment)
                    .HasName("dietsuppliment_pk");

                entity.ToTable("dietsuppliment");

                entity.Property(e => e.Iddietsuppliment).HasColumnName("iddietsuppliment");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Dose).HasColumnName("dose");

                entity.Property(e => e.Iddiet).HasColumnName("iddiet");

                entity.Property(e => e.Idsuppliment).HasColumnName("idsuppliment");

                entity.HasOne(d => d.IddietNavigation)
                    .WithMany(p => p.Dietsuppliments)
                    .HasForeignKey(d => d.Iddiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("diet_suppliment_diet");

                entity.HasOne(d => d.IdsupplimentNavigation)
                    .WithMany(p => p.Dietsuppliments)
                    .HasForeignKey(d => d.Idsuppliment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("diet_suppliment_suppliment");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.HasKey(e => e.Iddisease)
                    .HasName("diseases_pk");

                entity.ToTable("diseases");

                entity.Property(e => e.Iddisease).HasColumnName("iddisease");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(15000)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Recomendation)
                    .IsRequired()
                    .HasMaxLength(15000)
                    .HasColumnName("recomendation");
            });

            modelBuilder.Entity<DiseasePatient>(entity =>
            {
                entity.HasKey(e => e.IddiseasePatient)
                    .HasName("disease_patient_pk");

                entity.ToTable("disease_patient");

                entity.Property(e => e.IddiseasePatient).HasColumnName("iddisease_patient");

                entity.Property(e => e.Dateofcure)
                    .HasColumnType("date")
                    .HasColumnName("dateofcure");

                entity.Property(e => e.Dateofdiagnosis)
                    .HasColumnType("date")
                    .HasColumnName("dateofdiagnosis");

                entity.Property(e => e.Iddisease).HasColumnName("iddisease");

                entity.Property(e => e.Idpatient).HasColumnName("idpatient");

                entity.HasOne(d => d.IddiseaseNavigation)
                    .WithMany(p => p.DiseasePatients)
                    .HasForeignKey(d => d.Iddisease)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("disease_patient_diseases");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.DiseasePatients)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("disease_patient_patient");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("doctor_pk");

                entity.ToTable("doctor");

                entity.Property(e => e.Iduser)
                    .ValueGeneratedNever()
                    .HasColumnName("iduser");

                entity.Property(e => e.Office)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("office");

                entity.HasOne(d => d.IduserNavigation)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey<Doctor>(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_8_user");
            });

            modelBuilder.Entity<Foodinput>(entity =>
            {
                entity.HasKey(e => e.Idinput)
                    .HasName("foodinput_pk");

                entity.ToTable("foodinput");

                entity.Property(e => e.Idinput).HasColumnName("idinput");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Idpatient).HasColumnName("idpatient");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Time)
                    .HasColumnType("time without time zone")
                    .HasColumnName("time");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("unit");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Foodinputs)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("foodinput_patient");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.Foodinputs)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("foodinput_products");
            });

            modelBuilder.Entity<Individualrecipe>(entity =>
            {
                entity.HasKey(e => e.Idindividualrecipe)
                    .HasName("individualrecipe_pk");

                entity.ToTable("individualrecipe");

                entity.Property(e => e.Idindividualrecipe).HasColumnName("idindividualrecipe");

                entity.Property(e => e.Idmealtake).HasColumnName("idmealtake");

                entity.Property(e => e.Idrecipe).HasColumnName("idrecipe");

                entity.Property(e => e.Proportion).HasColumnName("proportion");

                entity.HasOne(d => d.IdmealtakeNavigation)
                    .WithMany(p => p.Individualrecipes)
                    .HasForeignKey(d => d.Idmealtake)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("individualrecipe_mealtake");

                entity.HasOne(d => d.IdrecipeNavigation)
                    .WithMany(p => p.Individualrecipes)
                    .HasForeignKey(d => d.Idrecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("indvidualrecipe_recipe");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.HasKey(e => e.Idmeal)
                    .HasName("meal_pk");

                entity.ToTable("meal");

                entity.Property(e => e.Idmeal).HasColumnName("idmeal");

                entity.Property(e => e.CookingUrl)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("cooking_url");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(15000)
                    .HasColumnName("description");

                entity.Property(e => e.Nameofmeal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameofmeal");
            });

            modelBuilder.Entity<Mealsbeforediet>(entity =>
            {
                entity.HasKey(e => e.Idmeal)
                    .HasName("mealsbeforediet_pk");

                entity.ToTable("mealsbeforediet");

                entity.Property(e => e.Idmeal).HasColumnName("idmeal");

                entity.Property(e => e.Foodtoeat)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("foodtoeat");

                entity.Property(e => e.Hour)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("hour");

                entity.Property(e => e.Idquestionary).HasColumnName("idquestionary");

                entity.Property(e => e.Mealnumber).HasColumnName("mealnumber");

                entity.HasOne(d => d.IdquestionaryNavigation)
                    .WithMany(p => p.Mealsbeforediets)
                    .HasForeignKey(d => d.Idquestionary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mealseat_pendinguser");
            });

            modelBuilder.Entity<Mealtake>(entity =>
            {
                entity.HasKey(e => e.Idmealtake)
                    .HasName("mealtake_pk");

                entity.ToTable("mealtake");

                entity.Property(e => e.Idmealtake).HasColumnName("idmealtake");

                entity.Property(e => e.Idday).HasColumnName("idday");

                entity.Property(e => e.Time)
                    .HasColumnType("time without time zone")
                    .HasColumnName("time");

                entity.HasOne(d => d.IddayNavigation)
                    .WithMany(p => p.Mealtakes)
                    .HasForeignKey(d => d.Idday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mealtake_day");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.HasKey(e => e.Idmeasurement)
                    .HasName("measurements_pk");

                entity.ToTable("measurements");

                entity.Property(e => e.Idmeasurement).HasColumnName("idmeasurement");

                entity.Property(e => e.Bicepscircumference).HasColumnName("bicepscircumference");

                entity.Property(e => e.Calfcircumference).HasColumnName("calfcircumference");

                entity.Property(e => e.Chestcircumference).HasColumnName("chestcircumference");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Height)
                    .HasPrecision(4, 1)
                    .HasColumnName("height");

                entity.Property(e => e.Hipcircumference)
                    .HasPrecision(4, 1)
                    .HasColumnName("hipcircumference");

                entity.Property(e => e.Idpatient).HasColumnName("idpatient");

                entity.Property(e => e.Thighcircumference).HasColumnName("thighcircumference");

                entity.Property(e => e.Waistcircumference)
                    .HasPrecision(4, 1)
                    .HasColumnName("waistcircumference");

                entity.Property(e => e.Waistlowercircumference).HasColumnName("waistlowercircumference");

                entity.Property(e => e.Weight)
                    .HasPrecision(4, 1)
                    .HasColumnName("weight");

                entity.Property(e => e.Whomeasured)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("whomeasured");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("measurements_patient");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Idnote)
                    .HasName("note_pk");

                entity.ToTable("note");

                entity.Property(e => e.Idnote).HasColumnName("idnote");

                entity.Property(e => e.Dateofnote)
                    .HasColumnType("date")
                    .HasColumnName("dateofnote");

                entity.Property(e => e.Iddoctor)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("iddoctor");

                entity.Property(e => e.Idpatient)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idpatient");

                entity.HasOne(d => d.IddoctorNavigation)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.Iddoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("note_doctor");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("note_patient");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasKey(e => e.Idparameter)
                    .HasName("parameter_pk");

                entity.ToTable("parameter");

                entity.Property(e => e.Idparameter).HasColumnName("idparameter");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("unit");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("patient_pk");

                entity.ToTable("patient");

                entity.Property(e => e.Iduser)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("iduser");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Flatnumber)
                    .HasMaxLength(10)
                    .HasColumnName("flatnumber");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("gender");

                entity.Property(e => e.Ispending).HasColumnName("ispending");

                entity.Property(e => e.Pal)
                    .HasPrecision(3, 2)
                    .HasColumnName("pal");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("street");

                entity.Property(e => e.Streetnumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("streetnumber");

                entity.HasOne(d => d.IduserNavigation)
                    .WithOne(p => p.Patient)
                    .HasForeignKey<Patient>(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("patient_user");
            });

            modelBuilder.Entity<PgStatStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnType("oid")
                    .HasColumnName("dbid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxTime).HasColumnName("max_time");

                entity.Property(e => e.MeanTime).HasColumnName("mean_time");

                entity.Property(e => e.MinTime).HasColumnName("min_time");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevTime).HasColumnName("stddev_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Userid)
                    .HasColumnType("oid")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Idproduct)
                    .HasName("product_pk");

                entity.ToTable("product");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Homemeasure).HasColumnName("homemeasure");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Servingsizeingramms).HasColumnName("servingsizeingramms");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("unit");
            });

            modelBuilder.Entity<ProductParameter>(entity =>
            {
                entity.HasKey(e => e.IdproductParameter)
                    .HasName("product_parameter_pk");

                entity.ToTable("product_parameter");

                entity.Property(e => e.IdproductParameter).HasColumnName("idproduct_parameter");

                entity.Property(e => e.Amount)
                    .HasPrecision(11, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.Idparameter).HasColumnName("idparameter");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.HasOne(d => d.IdparameterNavigation)
                    .WithMany(p => p.ProductParameters)
                    .HasForeignKey(d => d.Idparameter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_parameter_parameter");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.ProductParameters)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_parameter_products");
            });

            modelBuilder.Entity<Productdiet>(entity =>
            {
                entity.HasKey(e => e.IdproductDiet)
                    .HasName("productdiet_pk");

                entity.ToTable("productdiet");

                entity.Property(e => e.IdproductDiet).HasColumnName("idproduct_diet");

                entity.Property(e => e.Allowed).HasColumnName("allowed");

                entity.Property(e => e.Iddiet).HasColumnName("iddiet");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.HasOne(d => d.IddietNavigation)
                    .WithMany(p => p.Productdiets)
                    .HasForeignKey(d => d.Iddiet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_16_diet");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.Productdiets)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_16_products");
            });

            modelBuilder.Entity<Questionary>(entity =>
            {
                entity.HasKey(e => e.Idquestionary)
                    .HasName("questionary_pk");

                entity.ToTable("questionary");

                entity.Property(e => e.Idquestionary).HasColumnName("idquestionary");

                entity.Property(e => e.Afternoonmeal).HasColumnName("afternoonmeal");

                entity.Property(e => e.Alcoholinfo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("alcoholinfo");

                entity.Property(e => e.Alergieproducts)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("alergieproducts");

                entity.Property(e => e.Avgsleep)
                    .HasPrecision(4, 1)
                    .HasColumnName("avgsleep");

                entity.Property(e => e.Betweenmealsfood)
                    .HasMaxLength(150)
                    .HasColumnName("betweenmealsfood");

                entity.Property(e => e.Breakfast).HasColumnName("breakfast");

                entity.Property(e => e.Cigs).HasColumnName("cigs");

                entity.Property(e => e.Coffeeglasses).HasColumnName("coffeeglasses");

                entity.Property(e => e.Databadania)
                    .HasColumnType("date")
                    .HasColumnName("databadania");

                entity.Property(e => e.Diabetes).HasColumnName("diabetes");

                entity.Property(e => e.Dinner).HasColumnName("dinner");

                entity.Property(e => e.Education)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("education");

                entity.Property(e => e.Energydrinkglasses).HasColumnName("energydrinkglasses");

                entity.Property(e => e.Excercisingperday)
                    .HasPrecision(3, 2)
                    .HasColumnName("excercisingperday");

                entity.Property(e => e.Exercisingperweek)
                    .HasPrecision(3, 2)
                    .HasColumnName("exercisingperweek");

                entity.Property(e => e.Favfooditems)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("favfooditems");

                entity.Property(e => e.Hypersensitivityproducts)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("hypersensitivityproducts");

                entity.Property(e => e.Hypertension).HasColumnName("hypertension");

                entity.Property(e => e.Hypothyroidism).HasColumnName("hypothyroidism");

                entity.Property(e => e.Idpatient).HasColumnName("idpatient");

                entity.Property(e => e.Insulinresistance).HasColumnName("insulinresistance");

                entity.Property(e => e.Intestinaldiseases).HasColumnName("intestinaldiseases");

                entity.Property(e => e.Juiceglasses).HasColumnName("juiceglasses");

                entity.Property(e => e.Lunch).HasColumnName("lunch");

                entity.Property(e => e.Mainproblems)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("mainproblems");

                entity.Property(e => e.Medications)
                    .HasMaxLength(150)
                    .HasColumnName("medications");

                entity.Property(e => e.Notfavfooditems)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("notfavfooditems");

                entity.Property(e => e.Otherdiseases)
                    .HasMaxLength(300)
                    .HasColumnName("otherdiseases");

                entity.Property(e => e.Profession)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("profession");

                entity.Property(e => e.Regularwalk).HasColumnName("regularwalk");

                entity.Property(e => e.Secondbreakfast).HasColumnName("secondbreakfast");

                entity.Property(e => e.Sporttypes)
                    .HasMaxLength(300)
                    .HasColumnName("sporttypes");

                entity.Property(e => e.Supplementstaken)
                    .HasMaxLength(300)
                    .HasColumnName("supplementstaken");

                entity.Property(e => e.Teaglasses).HasColumnName("teaglasses");

                entity.Property(e => e.Usuallygotosleep)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("usuallygotosleep");

                entity.Property(e => e.Usuallywakeup)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("usuallywakeup");

                entity.Property(e => e.Waterglasses).HasColumnName("waterglasses");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Questionaries)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("questionary_patient");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Idrecipe)
                    .HasName("recipe_pk");

                entity.ToTable("recipe");

                entity.Property(e => e.Idrecipe).HasColumnName("idrecipe");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Idmeal).HasColumnName("idmeal");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.HasOne(d => d.IdmealNavigation)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.Idmeal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_meals");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_products");
            });

            modelBuilder.Entity<Supplement>(entity =>
            {
                entity.HasKey(e => e.Idsuppliment)
                    .HasName("supplement_pk");

                entity.ToTable("supplement");

                entity.Property(e => e.Idsuppliment).HasColumnName("idsuppliment");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TempUser>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("temp_user_pk");

                entity.ToTable("temp_user");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Uniquekey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("uniquekey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("users_pk");

                entity.ToTable("users");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnType("date")
                    .HasColumnName("dateofbirth");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("pesel");

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Refreshtoken)
                    .HasMaxLength(300)
                    .HasColumnName("refreshtoken");

                entity.Property(e => e.Refreshtokenexp).HasColumnName("refreshtokenexp");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("role");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("salt");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => e.Idvisit)
                    .HasName("visit_pk");

                entity.ToTable("visit");

                entity.Property(e => e.Idvisit).HasColumnName("idvisit");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("description");

                entity.Property(e => e.Iddoctor)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("iddoctor");

                entity.Property(e => e.Idpatient)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idpatient");

                entity.Property(e => e.Time)
                    .HasColumnType("time without time zone")
                    .HasColumnName("time");

                entity.HasOne(d => d.IddoctorNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.Iddoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visit_doctor");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.Idpatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visit_patient");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
