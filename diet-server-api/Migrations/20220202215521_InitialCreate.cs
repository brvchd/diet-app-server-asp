using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace diet_server_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:btree_gin", ",,")
                .Annotation("Npgsql:PostgresExtension:btree_gist", ",,")
                .Annotation("Npgsql:PostgresExtension:citext", ",,")
                .Annotation("Npgsql:PostgresExtension:cube", ",,")
                .Annotation("Npgsql:PostgresExtension:dblink", ",,")
                .Annotation("Npgsql:PostgresExtension:dict_int", ",,")
                .Annotation("Npgsql:PostgresExtension:dict_xsyn", ",,")
                .Annotation("Npgsql:PostgresExtension:earthdistance", ",,")
                .Annotation("Npgsql:PostgresExtension:fuzzystrmatch", ",,")
                .Annotation("Npgsql:PostgresExtension:hstore", ",,")
                .Annotation("Npgsql:PostgresExtension:intarray", ",,")
                .Annotation("Npgsql:PostgresExtension:ltree", ",,")
                .Annotation("Npgsql:PostgresExtension:pg_stat_statements", ",,")
                .Annotation("Npgsql:PostgresExtension:pg_trgm", ",,")
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,")
                .Annotation("Npgsql:PostgresExtension:pgrowlocks", ",,")
                .Annotation("Npgsql:PostgresExtension:pgstattuple", ",,")
                .Annotation("Npgsql:PostgresExtension:tablefunc", ",,")
                .Annotation("Npgsql:PostgresExtension:unaccent", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .Annotation("Npgsql:PostgresExtension:xml2", ",,");

            migrationBuilder.CreateTable(
                name: "diseases",
                columns: table => new
                {
                    iddisease = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(15000)", maxLength: 15000, nullable: false),
                    recomendation = table.Column<string>(type: "character varying(15000)", maxLength: 15000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("diseases_pk", x => x.iddisease);
                });

            migrationBuilder.CreateTable(
                name: "meal",
                columns: table => new
                {
                    idmeal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameofmeal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(15000)", maxLength: 15000, nullable: false),
                    cooking_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("meal_pk", x => x.idmeal);
                });

            migrationBuilder.CreateTable(
                name: "parameter",
                columns: table => new
                {
                    idparameter = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    measureunit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("parameter_pk", x => x.idparameter);
                });

            migrationBuilder.CreateTable(
                name: "pg_stat_statements",
                columns: table => new
                {
                    userid = table.Column<uint>(type: "oid", nullable: true),
                    dbid = table.Column<uint>(type: "oid", nullable: true),
                    queryid = table.Column<long>(type: "bigint", nullable: true),
                    query = table.Column<string>(type: "text", nullable: true),
                    calls = table.Column<long>(type: "bigint", nullable: true),
                    total_time = table.Column<double>(type: "double precision", nullable: true),
                    min_time = table.Column<double>(type: "double precision", nullable: true),
                    max_time = table.Column<double>(type: "double precision", nullable: true),
                    mean_time = table.Column<double>(type: "double precision", nullable: true),
                    stddev_time = table.Column<double>(type: "double precision", nullable: true),
                    rows = table.Column<long>(type: "bigint", nullable: true),
                    shared_blks_hit = table.Column<long>(type: "bigint", nullable: true),
                    shared_blks_read = table.Column<long>(type: "bigint", nullable: true),
                    shared_blks_dirtied = table.Column<long>(type: "bigint", nullable: true),
                    shared_blks_written = table.Column<long>(type: "bigint", nullable: true),
                    local_blks_hit = table.Column<long>(type: "bigint", nullable: true),
                    local_blks_read = table.Column<long>(type: "bigint", nullable: true),
                    local_blks_dirtied = table.Column<long>(type: "bigint", nullable: true),
                    local_blks_written = table.Column<long>(type: "bigint", nullable: true),
                    temp_blks_read = table.Column<long>(type: "bigint", nullable: true),
                    temp_blks_written = table.Column<long>(type: "bigint", nullable: true),
                    blk_read_time = table.Column<double>(type: "double precision", nullable: true),
                    blk_write_time = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    idproduct = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    size = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    homemeasure = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    homemeasuresize = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pk", x => x.idproduct);
                });

            migrationBuilder.CreateTable(
                name: "supplement",
                columns: table => new
                {
                    idsuppliment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("supplement_pk", x => x.idsuppliment);
                });

            migrationBuilder.CreateTable(
                name: "temp_user",
                columns: table => new
                {
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    uniquekey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("temp_user_pk", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    lastname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    dateofbirth = table.Column<DateTime>(type: "date", nullable: false),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    phonenumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    pesel = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    role = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    salt = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    refreshtoken = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    refreshtokenexp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pk", x => x.iduser);
                });

            migrationBuilder.CreateTable(
                name: "product_parameter",
                columns: table => new
                {
                    idproduct_parameter = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idproduct = table.Column<int>(type: "integer", nullable: false),
                    idparameter = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_parameter_pk", x => x.idproduct_parameter);
                    table.ForeignKey(
                        name: "product_parameter_parameter",
                        column: x => x.idparameter,
                        principalTable: "parameter",
                        principalColumn: "idparameter",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "product_parameter_products",
                        column: x => x.idproduct,
                        principalTable: "product",
                        principalColumn: "idproduct",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    idrecipe = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idproduct = table.Column<int>(type: "integer", nullable: false),
                    idmeal = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipe_pk", x => x.idrecipe);
                    table.ForeignKey(
                        name: "recipe_meals",
                        column: x => x.idmeal,
                        principalTable: "meal",
                        principalColumn: "idmeal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "recipe_products",
                        column: x => x.idproduct,
                        principalTable: "product",
                        principalColumn: "idproduct",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "doctor",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "integer", nullable: false),
                    office = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("doctor_pk", x => x.iduser);
                    table.ForeignKey(
                        name: "table_8_user",
                        column: x => x.iduser,
                        principalTable: "users",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "integer", nullable: false),
                    ispending = table.Column<bool>(type: "boolean", nullable: false),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    flatnumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    pal = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    streetnumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    correctedvalue = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    cpm = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("patient_pk", x => x.iduser);
                    table.ForeignKey(
                        name: "patient_user",
                        column: x => x.iduser,
                        principalTable: "users",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "diet",
                columns: table => new
                {
                    iddiet = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpatient = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(15000)", maxLength: 15000, nullable: false),
                    datefrom = table.Column<DateTime>(type: "date", nullable: false),
                    dateto = table.Column<DateTime>(type: "date", nullable: false),
                    dailymeals = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    protein = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    changesdate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("diet_pk", x => x.iddiet);
                    table.ForeignKey(
                        name: "diet_patient",
                        column: x => x.idpatient,
                        principalTable: "patient",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "disease_patient",
                columns: table => new
                {
                    iddisease_patient = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddisease = table.Column<int>(type: "integer", nullable: false),
                    idpatient = table.Column<int>(type: "integer", nullable: false),
                    dateofdiagnosis = table.Column<DateTime>(type: "date", nullable: true),
                    dateofcure = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("disease_patient_pk", x => x.iddisease_patient);
                    table.ForeignKey(
                        name: "disease_patient_diseases",
                        column: x => x.iddisease,
                        principalTable: "diseases",
                        principalColumn: "iddisease",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "disease_patient_patient",
                        column: x => x.idpatient,
                        principalTable: "patient",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "measurements",
                columns: table => new
                {
                    idmeasurement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpatient = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    weight = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    hipcircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    waistcircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    bicepscircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    chestcircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    thighcircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    calfcircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    waistlowercircumference = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    whomeasured = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("measurements_pk", x => x.idmeasurement);
                    table.ForeignKey(
                        name: "measurements_patient",
                        column: x => x.idpatient,
                        principalTable: "patient",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "note",
                columns: table => new
                {
                    idnote = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpatient = table.Column<int>(type: "integer", nullable: false),
                    iddoctor = table.Column<int>(type: "integer", nullable: false),
                    dateofnote = table.Column<DateTime>(type: "date", nullable: false),
                    message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("note_pk", x => x.idnote);
                    table.ForeignKey(
                        name: "note_doctor",
                        column: x => x.iddoctor,
                        principalTable: "doctor",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "note_patient",
                        column: x => x.idpatient,
                        principalTable: "patient",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "questionnaire",
                columns: table => new
                {
                    idquestionary = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idpatient = table.Column<int>(type: "integer", nullable: false),
                    databadania = table.Column<DateTime>(type: "date", nullable: false),
                    education = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    profession = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mainproblems = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    hypertension = table.Column<bool>(type: "boolean", nullable: false),
                    insulinresistance = table.Column<bool>(type: "boolean", nullable: false),
                    diabetes = table.Column<bool>(type: "boolean", nullable: false),
                    hypothyroidism = table.Column<bool>(type: "boolean", nullable: false),
                    intestinaldiseases = table.Column<bool>(type: "boolean", nullable: false),
                    otherdiseases = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    medications = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    supplementstaken = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    avgsleep = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    usuallywakeup = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    usuallygotosleep = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    regularwalk = table.Column<bool>(type: "boolean", nullable: false),
                    excercisingperday = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    sporttypes = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    exercisingperweek = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    waterglasses = table.Column<int>(type: "integer", nullable: false),
                    coffeeglasses = table.Column<int>(type: "integer", nullable: false),
                    teaglasses = table.Column<int>(type: "integer", nullable: false),
                    juiceglasses = table.Column<int>(type: "integer", nullable: false),
                    energydrinkglasses = table.Column<int>(type: "integer", nullable: false),
                    alcoholinfo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cigs = table.Column<int>(type: "integer", nullable: false),
                    breakfast = table.Column<bool>(type: "boolean", nullable: false),
                    secondbreakfast = table.Column<bool>(type: "boolean", nullable: false),
                    lunch = table.Column<bool>(type: "boolean", nullable: false),
                    afternoonmeal = table.Column<bool>(type: "boolean", nullable: false),
                    dinner = table.Column<bool>(type: "boolean", nullable: false),
                    favfooditems = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    notfavfooditems = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    hypersensitivityproducts = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    alergieproducts = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    betweenmealsfood = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("questionnaire_pk", x => x.idquestionary);
                    table.ForeignKey(
                        name: "questionary_patient",
                        column: x => x.idpatient,
                        principalTable: "patient",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "visit",
                columns: table => new
                {
                    idvisit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddoctor = table.Column<int>(type: "integer", nullable: false),
                    idpatient = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    time = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("visit_pk", x => x.idvisit);
                    table.ForeignKey(
                        name: "visit_doctor",
                        column: x => x.iddoctor,
                        principalTable: "doctor",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "visit_patient",
                        column: x => x.idpatient,
                        principalTable: "patient",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "day",
                columns: table => new
                {
                    idday = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    daynumber = table.Column<int>(type: "integer", nullable: false),
                    dietiddiet = table.Column<int>(type: "integer", nullable: false),
                    patientreport = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("day_pk", x => x.idday);
                    table.ForeignKey(
                        name: "day_diet",
                        column: x => x.dietiddiet,
                        principalTable: "diet",
                        principalColumn: "iddiet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dietsuppliment",
                columns: table => new
                {
                    iddietsuppliment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddiet = table.Column<int>(type: "integer", nullable: false),
                    idsuppliment = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("dietsuppliment_pk", x => x.iddietsuppliment);
                    table.ForeignKey(
                        name: "diet_suppliment_diet",
                        column: x => x.iddiet,
                        principalTable: "diet",
                        principalColumn: "iddiet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "diet_suppliment_suppliment",
                        column: x => x.idsuppliment,
                        principalTable: "supplement",
                        principalColumn: "idsuppliment",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mealsbeforediet",
                columns: table => new
                {
                    idmeal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idquestionary = table.Column<int>(type: "integer", nullable: false),
                    mealnumber = table.Column<int>(type: "integer", nullable: false),
                    hour = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    foodtoeat = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("mealsbeforediet_pk", x => x.idmeal);
                    table.ForeignKey(
                        name: "mealseat_pendinguser",
                        column: x => x.idquestionary,
                        principalTable: "questionnaire",
                        principalColumn: "idquestionary",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mealtake",
                columns: table => new
                {
                    idmealtake = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idday = table.Column<int>(type: "integer", nullable: false),
                    time = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    isfollowed = table.Column<bool>(type: "boolean", nullable: true),
                    proportion = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("mealtake_pk", x => x.idmealtake);
                    table.ForeignKey(
                        name: "mealtake_day",
                        column: x => x.idday,
                        principalTable: "day",
                        principalColumn: "idday",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "individualrecipe",
                columns: table => new
                {
                    idindividualrecipe = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idrecipe = table.Column<int>(type: "integer", nullable: false),
                    idmealtake = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("individualrecipe_pk", x => x.idindividualrecipe);
                    table.ForeignKey(
                        name: "individualrecipe_mealtake",
                        column: x => x.idmealtake,
                        principalTable: "mealtake",
                        principalColumn: "idmealtake",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "indvidualrecipe_recipe",
                        column: x => x.idrecipe,
                        principalTable: "recipe",
                        principalColumn: "idrecipe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_day_dietiddiet",
                table: "day",
                column: "dietiddiet");

            migrationBuilder.CreateIndex(
                name: "IX_diet_idpatient",
                table: "diet",
                column: "idpatient");

            migrationBuilder.CreateIndex(
                name: "IX_dietsuppliment_iddiet",
                table: "dietsuppliment",
                column: "iddiet");

            migrationBuilder.CreateIndex(
                name: "IX_dietsuppliment_idsuppliment",
                table: "dietsuppliment",
                column: "idsuppliment");

            migrationBuilder.CreateIndex(
                name: "IX_disease_patient_iddisease",
                table: "disease_patient",
                column: "iddisease");

            migrationBuilder.CreateIndex(
                name: "IX_disease_patient_idpatient",
                table: "disease_patient",
                column: "idpatient");

            migrationBuilder.CreateIndex(
                name: "IX_individualrecipe_idmealtake",
                table: "individualrecipe",
                column: "idmealtake");

            migrationBuilder.CreateIndex(
                name: "IX_individualrecipe_idrecipe",
                table: "individualrecipe",
                column: "idrecipe");

            migrationBuilder.CreateIndex(
                name: "IX_mealsbeforediet_idquestionary",
                table: "mealsbeforediet",
                column: "idquestionary");

            migrationBuilder.CreateIndex(
                name: "IX_mealtake_idday",
                table: "mealtake",
                column: "idday");

            migrationBuilder.CreateIndex(
                name: "IX_measurements_idpatient",
                table: "measurements",
                column: "idpatient");

            migrationBuilder.CreateIndex(
                name: "IX_note_iddoctor",
                table: "note",
                column: "iddoctor");

            migrationBuilder.CreateIndex(
                name: "IX_note_idpatient",
                table: "note",
                column: "idpatient");

            migrationBuilder.CreateIndex(
                name: "IX_product_parameter_idparameter",
                table: "product_parameter",
                column: "idparameter");

            migrationBuilder.CreateIndex(
                name: "IX_product_parameter_idproduct",
                table: "product_parameter",
                column: "idproduct");

            migrationBuilder.CreateIndex(
                name: "IX_questionnaire_idpatient",
                table: "questionnaire",
                column: "idpatient");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_idmeal",
                table: "recipe",
                column: "idmeal");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_idproduct",
                table: "recipe",
                column: "idproduct");

            migrationBuilder.CreateIndex(
                name: "IX_visit_iddoctor",
                table: "visit",
                column: "iddoctor");

            migrationBuilder.CreateIndex(
                name: "IX_visit_idpatient",
                table: "visit",
                column: "idpatient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dietsuppliment");

            migrationBuilder.DropTable(
                name: "disease_patient");

            migrationBuilder.DropTable(
                name: "individualrecipe");

            migrationBuilder.DropTable(
                name: "mealsbeforediet");

            migrationBuilder.DropTable(
                name: "measurements");

            migrationBuilder.DropTable(
                name: "note");

            migrationBuilder.DropTable(
                name: "pg_stat_statements");

            migrationBuilder.DropTable(
                name: "product_parameter");

            migrationBuilder.DropTable(
                name: "temp_user");

            migrationBuilder.DropTable(
                name: "visit");

            migrationBuilder.DropTable(
                name: "supplement");

            migrationBuilder.DropTable(
                name: "diseases");

            migrationBuilder.DropTable(
                name: "mealtake");

            migrationBuilder.DropTable(
                name: "recipe");

            migrationBuilder.DropTable(
                name: "questionnaire");

            migrationBuilder.DropTable(
                name: "parameter");

            migrationBuilder.DropTable(
                name: "doctor");

            migrationBuilder.DropTable(
                name: "day");

            migrationBuilder.DropTable(
                name: "meal");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "diet");

            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
