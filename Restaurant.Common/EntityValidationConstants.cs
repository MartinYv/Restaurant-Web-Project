﻿namespace Restaurant.Common
{
    public static class EntityValidationConstants
    {
        public static class Dish
        {
            public const int DishNameMinLength = 4;
            public const int DishNameMaxLength = 20;

            public const int DishDescriptionMinLength = 20;
            public const int DishDescriptionMaxLength = 20;

            public const int DishPriceMinLength = 5;
            public const int DishPriceMaxLength = 50;


            public const int DishUrlMaxLength = 2048;
        }
        public static class DishType
        {
            public const int DishTypeMinLength = 4;
            public const int DishTypeMaxLenght = 15;
        }

        public static class Table
        {
            public const int SeatsMinLength = 2;
            public const int SeatsMaxLength = 10;

        }

        public static class Order
        {
            public const int PhoneMinLength = 7;
            public const int PhoneMaxLength = 20;

            public const int AddressMinLength = 6;
            public const int AddressMaxLength = 30;
        }
        public static class Menu
        {
            public const int MenuTypeMinLenght = 3;
            public const int MenuTypeMaxLenght = 15;
        }
        public static class MenuType
        {
            public const int MenuTypeMinLenght = 3;
            public const int MenuTypeMaxLenght = 15;
        }
        
        public static class Reservation
        {
			public const decimal ReservationHourMinLength = 9;
			public const decimal ReservationHourMaxLength = 22;

		}
	}
}