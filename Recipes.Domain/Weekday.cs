using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public enum WeekdayEnum
    {
        Unknown = 0,
        Sunday = 1,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    };

    public class Weekday
    {
        private Weekday(WeekdayEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected Weekday() { } //For EF

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        public static implicit operator Weekday(WeekdayEnum @enum) => new Weekday(@enum);

        public static implicit operator WeekdayEnum(Weekday weekDay) => (WeekdayEnum)weekDay.Id;
    }

    public static class Extensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item)
            => item.GetType()
                   .GetField(item.ToString())
                   .GetCustomAttributes(typeof(DescriptionAttribute), false)
                   .Cast<DescriptionAttribute>()
                   .FirstOrDefault()?.Description ?? string.Empty;

    }//class
}//ns
