using System;
using static diet_server_api.DTO.Requests.SurveySignUpRequest;

namespace diet_server_api.DTO.Responses.Doctor.Get
{
    public class GetPatientInfoResponse
    {
        public DateTime DateOfSurvey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PESEL { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal IdealBodyWeight { get; set; }
        public decimal ModifiedFormula { get; set; }
        public decimal BasicMetabolism { get; set; }
        public decimal WaistCircumference { get; set; }
        public decimal HipCircumference { get; set; }
        public decimal WaistHipRatio { get; set; }
        public string ConsultationGoal { get; set; }
        public bool Diabetes { get; set; }
        public bool Hypertension { get; set; }
        public bool InsulinResistance { get; set; }
        public bool Hypothyroidism { get; set; }
        public bool IntestinalDiseases { get; set; }
        public string OtherDiseases { get; set; }
        public string Medications { get; set; }
        public string DietSupplements { get; set; }
        public string GetUpInterval { get; set; }
        public string GoToSleepInterval { get; set; }
        public decimal AvgSleep { get; set; }
        public decimal SportsPerDay { get; set; }
        public decimal SportsPerWeek { get; set; }
        public bool RegularWalk { get; set; }
        public string SportTypes { get; set; }
        public int WaterGlasses { get; set; }
        public int CoffeeGlasses { get; set; }
        public int TeaGlasses { get; set; }
        public int JuiceGlasses { get; set; }
        public int EnergyDrinkGlasses { get; set; }
        public string Alcohol { get; set; }
        public int Cigs { get; set; }
        public bool Breakfast { get; set; }
        public bool SecondBreakfast { get; set; }
        public bool Lunch { get; set; }
        public bool AfternoonMeal { get; set; }
        public bool Dinner { get; set; }
        public string FavFood { get; set; }
        public string NotFavFood { get; set; }
        public string HypersensProds { get; set; }
        public string AlergieProds { get; set; }
        public MealsBeforeDiet[] Meals { get; set; }
        public string FoodBetweenMeals { get; set; }
        public decimal? PAL { get; set; }
        public decimal? CorrectedValue { get; set; }
        public decimal? CPM { get; set; }
    }
}
