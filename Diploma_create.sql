-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2021-11-07 16:49:14.962

-- tables
-- Table: Day
CREATE TABLE Day (
    idDay serial  NOT NULL,
    dayNumber int  NOT NULL,
    Diet_idDiet int  NOT NULL,
    CONSTRAINT Day_pk PRIMARY KEY (idDay)
);

-- Table: Diet
CREATE TABLE Diet (
    idDiet serial  NOT NULL,
    idPatient int  NOT NULL,
    Name varchar(30)  NOT NULL,
    Description varchar(15000)  NOT NULL,
    DateFrom date  NOT NULL,
    DateTo date  NOT NULL,
    DateToInformAboutChanges date  NULL,
    numberOfMealsPerDay int  NOT NULL,
    TotalAmountOfCalories decimal(10,2)  NOT NULL,
    Carbs int  NOT NULL,
    Fat int  NOT NULL,
    Fiber int  NOT NULL,
    Protein int  NOT NULL,
    CONSTRAINT Diet_pk PRIMARY KEY (idDiet)
);

-- Table: DietSuppliment
CREATE TABLE DietSuppliment (
    idDietSuppliment serial  NOT NULL,
    idDiet int  NOT NULL,
    idSuppliment int  NOT NULL,
    Dose int  NOT NULL,
    Description varchar(150)  NOT NULL,
    CONSTRAINT DietSuppliment_pk PRIMARY KEY (idDietSuppliment)
);

-- Table: Disease_Patient
CREATE TABLE Disease_Patient (
    idDisease_Patient serial  NOT NULL,
    idDisease int  NOT NULL,
    idPatient int  NOT NULL,
    DateOfDiagnosis date  NULL,
    DateOfCure date  NULL,
    CONSTRAINT Disease_Patient_pk PRIMARY KEY (idDisease_Patient)
);

-- Table: Diseases
CREATE TABLE Diseases (
    idDisease serial  NOT NULL,
    Name varchar(30)  NOT NULL,
    Description varchar(15000)  NOT NULL,
    Recomendation varchar(15000)  NOT NULL,
    CONSTRAINT Diseases_pk PRIMARY KEY (idDisease)
);

-- Table: Doctor
CREATE TABLE Doctor (
    idUser int  NOT NULL,
    Office varchar(30)  NOT NULL,
    CONSTRAINT Doctor_pk PRIMARY KEY (idUser)
);

-- Table: FoodInput
CREATE TABLE FoodInput (
    idInput serial  NOT NULL,
    idProduct int  NOT NULL,
    idPatient int  NOT NULL,
    Name varchar(50)  NOT NULL,
    Amount int  NOT NULL,
    Unit varchar(30)  NOT NULL,
    Time time  NOT NULL,
    Date date  NOT NULL,
    CONSTRAINT FoodInput_pk PRIMARY KEY (idInput)
);

-- Table: IndividualRecipe
CREATE TABLE IndividualRecipe (
    idIndividualRecipe serial  NOT NULL,
    idRecipe int  NOT NULL,
    proportion int  NOT NULL,
    idMealTake int  NOT NULL,
    CONSTRAINT IndividualRecipe_pk PRIMARY KEY (idIndividualRecipe)
);

-- Table: Meal
CREATE TABLE Meal (
    idMeal serial  NOT NULL,
    NameOfMeal varchar(50)  NOT NULL,
    Description varchar(15000)  NOT NULL,
    Cooking_URL varchar(200)  NOT NULL,
    CONSTRAINT Meal_pk PRIMARY KEY (idMeal)
);

-- Table: MealTake
CREATE TABLE MealTake (
    idMealTake serial  NOT NULL,
    time time  NOT NULL,
    idDay int  NOT NULL,
    CONSTRAINT MealTake_pk PRIMARY KEY (idMealTake)
);

-- Table: MealsBeforeDiet
CREATE TABLE MealsBeforeDiet (
    IdMeal serial  NOT NULL,
    idQuestionary int  NOT NULL,
    MealNumber int  NOT NULL,
    Hour varchar(30)  NOT NULL,
    FoodToEat varchar(150)  NOT NULL,
    CONSTRAINT MealsBeforeDiet_pk PRIMARY KEY (IdMeal)
);

-- Table: Measurements
CREATE TABLE Measurements (
    idMeasurement serial  NOT NULL,
    idPatient int  NOT NULL,
    Height decimal(4,1)  NOT NULL,
    Weight decimal(4,1)  NOT NULL,
    Date date  NOT NULL,
    HipCircumference decimal(4,1)  NOT NULL,
    WaistCircumference decimal(4,1)  NOT NULL,
    BicepsCircumference int  NULL,
    ChestCircumference int  NULL,
    ThighCircumference int  NULL,
    CalfCircumference int  NULL,
    WaistLowerCircumference int  NULL,
    whoMeasured varchar(100)  NOT NULL,
    CONSTRAINT Measurements_pk PRIMARY KEY (idMeasurement)
);

-- Table: Note
CREATE TABLE Note (
    idNote serial  NOT NULL,
    idPatient serial  NOT NULL,
    idDoctor serial  NOT NULL,
    dateOfNote date  NOT NULL,
    CONSTRAINT Note_pk PRIMARY KEY (idNote)
);

-- Table: Parameter
CREATE TABLE Parameter (
    idParameter serial  NOT NULL,
    Name varchar(100)  NOT NULL,
    Unit varchar(100)  NOT NULL,
    CONSTRAINT Parameter_pk PRIMARY KEY (idParameter)
);

-- Table: Patient
CREATE TABLE Patient (
    idUser serial  NOT NULL,
    IsPending boolean  NOT NULL,
    Gender varchar(20)  NOT NULL,
    City varchar(50)  NOT NULL,
    Street varchar(50)  NOT NULL,
    FlatNumber varchar(10)  NULL,
    PAL decimal(3,2)  NULL,
    StreetNumber varchar(10)  NOT NULL,
    CONSTRAINT Patient_pk PRIMARY KEY (idUser)
);

-- Table: Product
CREATE TABLE Product (
    idProduct serial  NOT NULL,
    Name varchar(30)  NOT NULL,
    Unit varchar(50)  NOT NULL,
    servingSizeInGramms int  NOT NULL,
    homeMeasure int  NOT NULL,
    CONSTRAINT Product_pk PRIMARY KEY (idProduct)
);

-- Table: ProductDiet
CREATE TABLE ProductDiet (
    idProduct_Diet serial  NOT NULL,
    idProduct int  NOT NULL,
    idDiet int  NOT NULL,
    allowed boolean  NOT NULL,
    CONSTRAINT ProductDiet_pk PRIMARY KEY (idProduct_Diet)
);

-- Table: Product_Parameter
CREATE TABLE Product_Parameter (
    idProduct_Parameter serial  NOT NULL,
    idProduct int  NOT NULL,
    idParameter int  NOT NULL,
    Amount decimal(11,2)  NOT NULL,
    CONSTRAINT Product_Parameter_pk PRIMARY KEY (idProduct_Parameter)
);

-- Table: Questionary
CREATE TABLE Questionary (
    IdQuestionary serial  NOT NULL,
    idPatient int  NOT NULL,
    DataBadania date  NOT NULL,
    Education varchar(50)  NOT NULL,
    Profession varchar(50)  NOT NULL,
    MainProblems varchar(150)  NOT NULL,
    Hypertension boolean  NOT NULL,
    InsulinResistance boolean  NOT NULL,
    Diabetes boolean  NOT NULL,
    Hypothyroidism boolean  NOT NULL,
    IntestinalDiseases boolean  NOT NULL,
    OtherDiseases varchar(300)  NULL,
    Medications varchar(150)  NULL,
    SupplementsTaken varchar(300)  NULL,
    AvgSleep varchar(5)  NOT NULL,
    UsuallyWakeup varchar(10)  NOT NULL,
    UsuallyGoToSleep varchar(10)  NOT NULL,
    RegularWalk boolean  NOT NULL,
    ExcercisingPerDay decimal(3,2)  NOT NULL,
    SportTypes varchar(300)  NULL,
    ExercisingPerWeek decimal(3,2)  NOT NULL,
    WaterGlasses int  NOT NULL,
    CoffeeGlasses int  NOT NULL,
    TeaGlasses int  NOT NULL,
    JuiceGlasses int  NOT NULL,
    EnergyDrinkGlasses int  NOT NULL,
    AlcoholInfo varchar(150)  NOT NULL,
    Cigs int  NOT NULL,
    Breakfast boolean  NOT NULL,
    SecondBreakfast boolean  NOT NULL,
    Lunch boolean  NOT NULL,
    AfternoonMeal boolean  NOT NULL,
    Dinner boolean  NOT NULL,
    FavFoodItems varchar(150)  NOT NULL,
    NotFavFoodItems varchar(150)  NOT NULL,
    HypersensitivityProducts varchar(150)  NOT NULL,
    AlergieProducts varchar(150)  NOT NULL,
    BetweenMealsFood varchar(150)  NULL,
    CONSTRAINT Questionary_pk PRIMARY KEY (IdQuestionary)
);

-- Table: Recipe
CREATE TABLE Recipe (
    idRecipe serial  NOT NULL,
    idProduct int  NOT NULL,
    idMeal int  NOT NULL,
    Amount int  NOT NULL,
    CONSTRAINT Recipe_pk PRIMARY KEY (idRecipe)
);

-- Table: Supplement
CREATE TABLE Supplement (
    idSuppliment serial  NOT NULL,
    Name varchar(30)  NOT NULL,
    Description varchar(150)  NOT NULL,
    CONSTRAINT Supplement_pk PRIMARY KEY (idSuppliment)
);

-- Table: Temp_User
CREATE TABLE Temp_User (
    email varchar(255)  NOT NULL,
    uniqueKey varchar(255)  NOT NULL,
    Token varchar(255)  NOT NULL,
    CONSTRAINT Temp_User_pk PRIMARY KEY (email)
);

-- Table: Users
CREATE TABLE Users (
    idUser serial  NOT NULL,
    FirstName varchar(30)  NOT NULL,
    LastName varchar(30)  NOT NULL,
    DateOfBirth date  NOT NULL,
    Email varchar(60)  NOT NULL,
    PhoneNumber varchar(30)  NOT NULL,
    PESEL varchar(11)  NOT NULL,
    Role varchar(30)  NOT NULL,
    Password varchar(50)  NOT NULL,
    Salt varchar(150)  NOT NULL,
    RefreshToken varchar(300)  NULL,
    RefreshTokenExp timestamp  NULL,
    CONSTRAINT Users_pk PRIMARY KEY (idUser)
);

-- Table: Visit
CREATE TABLE Visit (
    idVisit serial  NOT NULL,
    idDoctor serial  NOT NULL,
    idPatient serial  NOT NULL,
    Date date  NOT NULL,
    Time time  NOT NULL,
    description varchar(300)  NOT NULL,
    CONSTRAINT Visit_pk PRIMARY KEY (idVisit)
);

-- foreign keys
-- Reference: Day_Diet (table: Day)
ALTER TABLE Day ADD CONSTRAINT Day_Diet
    FOREIGN KEY (Diet_idDiet)
    REFERENCES Diet (idDiet)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Diet_Patient (table: Diet)
ALTER TABLE Diet ADD CONSTRAINT Diet_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Diet_Suppliment_Diet (table: DietSuppliment)
ALTER TABLE DietSuppliment ADD CONSTRAINT Diet_Suppliment_Diet
    FOREIGN KEY (idDiet)
    REFERENCES Diet (idDiet)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Diet_Suppliment_Suppliment (table: DietSuppliment)
ALTER TABLE DietSuppliment ADD CONSTRAINT Diet_Suppliment_Suppliment
    FOREIGN KEY (idSuppliment)
    REFERENCES Supplement (idSuppliment)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Disease_Patient_Diseases (table: Disease_Patient)
ALTER TABLE Disease_Patient ADD CONSTRAINT Disease_Patient_Diseases
    FOREIGN KEY (idDisease)
    REFERENCES Diseases (idDisease)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Disease_Patient_Patient (table: Disease_Patient)
ALTER TABLE Disease_Patient ADD CONSTRAINT Disease_Patient_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: FoodInput_Patient (table: FoodInput)
ALTER TABLE FoodInput ADD CONSTRAINT FoodInput_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: FoodInput_Products (table: FoodInput)
ALTER TABLE FoodInput ADD CONSTRAINT FoodInput_Products
    FOREIGN KEY (idProduct)
    REFERENCES Product (idProduct)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: IndividualRecipe_MealTake (table: IndividualRecipe)
ALTER TABLE IndividualRecipe ADD CONSTRAINT IndividualRecipe_MealTake
    FOREIGN KEY (idMealTake)
    REFERENCES MealTake (idMealTake)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: MealTake_Day (table: MealTake)
ALTER TABLE MealTake ADD CONSTRAINT MealTake_Day
    FOREIGN KEY (idDay)
    REFERENCES Day (idDay)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: MealsEat_PendingUser (table: MealsBeforeDiet)
ALTER TABLE MealsBeforeDiet ADD CONSTRAINT MealsEat_PendingUser
    FOREIGN KEY (idQuestionary)
    REFERENCES Questionary (IdQuestionary)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Measurements_Patient (table: Measurements)
ALTER TABLE Measurements ADD CONSTRAINT Measurements_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Note_Doctor (table: Note)
ALTER TABLE Note ADD CONSTRAINT Note_Doctor
    FOREIGN KEY (idDoctor)
    REFERENCES Doctor (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Note_Patient (table: Note)
ALTER TABLE Note ADD CONSTRAINT Note_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Patient_User (table: Patient)
ALTER TABLE Patient ADD CONSTRAINT Patient_User
    FOREIGN KEY (idUser)
    REFERENCES Users (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Product_Parameter_Parameter (table: Product_Parameter)
ALTER TABLE Product_Parameter ADD CONSTRAINT Product_Parameter_Parameter
    FOREIGN KEY (idParameter)
    REFERENCES Parameter (idParameter)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Product_Parameter_Products (table: Product_Parameter)
ALTER TABLE Product_Parameter ADD CONSTRAINT Product_Parameter_Products
    FOREIGN KEY (idProduct)
    REFERENCES Product (idProduct)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Questionary_Patient (table: Questionary)
ALTER TABLE Questionary ADD CONSTRAINT Questionary_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Recipe_Meals (table: Recipe)
ALTER TABLE Recipe ADD CONSTRAINT Recipe_Meals
    FOREIGN KEY (idMeal)
    REFERENCES Meal (idMeal)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Recipe_Products (table: Recipe)
ALTER TABLE Recipe ADD CONSTRAINT Recipe_Products
    FOREIGN KEY (idProduct)
    REFERENCES Product (idProduct)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Table_16_Diet (table: ProductDiet)
ALTER TABLE ProductDiet ADD CONSTRAINT Table_16_Diet
    FOREIGN KEY (idDiet)
    REFERENCES Diet (idDiet)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Table_16_Products (table: ProductDiet)
ALTER TABLE ProductDiet ADD CONSTRAINT Table_16_Products
    FOREIGN KEY (idProduct)
    REFERENCES Product (idProduct)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Table_8_User (table: Doctor)
ALTER TABLE Doctor ADD CONSTRAINT Table_8_User
    FOREIGN KEY (idUser)
    REFERENCES Users (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Visit_Doctor (table: Visit)
ALTER TABLE Visit ADD CONSTRAINT Visit_Doctor
    FOREIGN KEY (idDoctor)
    REFERENCES Doctor (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Visit_Patient (table: Visit)
ALTER TABLE Visit ADD CONSTRAINT Visit_Patient
    FOREIGN KEY (idPatient)
    REFERENCES Patient (idUser)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: indvidualRecipe_Recipe (table: IndividualRecipe)
ALTER TABLE IndividualRecipe ADD CONSTRAINT indvidualRecipe_Recipe
    FOREIGN KEY (idRecipe)
    REFERENCES Recipe (idRecipe)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- End of file.

