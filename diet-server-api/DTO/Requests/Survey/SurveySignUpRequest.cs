using System;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests
{
    public class SurveySignUpRequest
    {
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string AccessEmail { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(60)]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Invalid PESEL format")]
        public string PESEL { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(30)]
        public string Gender { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(30)]
        public string FlatNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [MaxLength(10)]
        public string StreetNumber { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public decimal HipCircumference { get; set; }
        [Required]
        public decimal WaistCircumference { get; set; }
        [Required]
        [MaxLength(50)]
        public string Education { get; set; }
        [Required]
        [MaxLength(50)]
        public string Profession { get; set; }
        [Required]
        [MaxLength(150)]
        public string MainProblems { get; set; }
        [Required]
        public bool Hypertension { get; set; }
        [Required]
        public bool InsulinResistance { get; set; }
        [Required]
        public bool Diabetes { get; set; }
        [Required]
        public bool Hypothyroidism { get; set; }
        [Required]
        public bool IntestinalDiseases { get; set; }
        public string OtherDiseases { get; set; }
        public string Medications { get; set; }
        public string SupplementsTaken { get; set; }
        [Required]
        [MaxLength(5)]
        public string AvgSleep { get; set; }
        [Required]
        [MaxLength(10)]
        public string UsuallyWakeUp { get; set; }
        [Required]
        [MaxLength(10)]
        public string UsuallyGoToSleep { get; set; }
        [Required]
        public bool RegularWalk { get; set; }
        [Required]
        public decimal ExercisingPerDay { get; set; }
        public string SportTypes { get; set; }
        [Required]
        public decimal ExercisingPerWeek { get; set; }
        [Required]
        public int WaterGlasses { get; set; }
        [Required]
        public int CoffeeGlasses { get; set; }
        [Required]
        public int TeaGlasses { get; set; }
        [Required]
        public int JuiceGlasses { get; set; }
        [Required]
        public int EnergyDrinkGlasses { get; set; }
        [Required]
        public string AlcoholInfo { get; set; }
        [Required]
        public int Cigs { get; set; }
        [Required]
        public bool Breakfast { get; set; }
        [Required]
        public bool SecondBreakfast { get; set; }
        [Required]
        public bool Lunch { get; set; }
        [Required]
        public bool AfternoonMeal { get; set; }
        [Required]
        public bool Dinner { get; set; }
        [Required]
        [MaxLength(150)]
        public string FavouriteFoodItems { get; set; }
        [Required]
        [MaxLength(150)]
        public string NotFavouriteFoodItems { get; set; }
        [Required]
        [MaxLength(150)]
        public string HypersensitivityProducts { get; set; }
        [Required]
        [MaxLength(150)]
        public string AlergieProducts { get; set; }
        [Required]
        [MaxLength(150)]
        public string FoodBetweenMeals { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public MealsBeforeDiet[] Meals { get; set; }
        public class MealsBeforeDiet
        {
            [Required]
            public int MealNumber { get; set; }
            [Required]
            [MaxLength(30)]
            public string AtTime { get; set; }
            [MaxLength(150)]
            public string FoodToEat { get; set; }
        }
    }
}