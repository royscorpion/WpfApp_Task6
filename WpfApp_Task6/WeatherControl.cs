using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp_Task6
{
    class WeatherControl:DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;

        private string wind_direction;
        private int wind_speed;
        private Precipitation precipitation;
        
        public enum Precipitation
        {
            Sunny,
            Cloudy,
            Rain,
            Snow,
        }

        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }

        public string WindDirection
        {
            get => wind_direction;
            set => wind_direction = value;
        }

        public int WindSpeed
        {
            get => wind_speed;
            set => wind_speed = value;
        }
        
        public WeatherControl(int temperature, string wind_direction, int wind_speed, Precipitation precipitation)
        {
            this.Temperature = temperature;
            this.WindDirection = wind_direction;
            this.WindSpeed = wind_speed;
            this.precipitation = precipitation;
        }

        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure|
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                new ValidateValueCallback(ValidateTemperature));
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
                return v;
            else
                return 0;
        }

        private static bool ValidateTemperature(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
                return true;
            else
                return false;
        }

        public string Print()
        {
            return $"Температура: {Temperature}, Ветер: {WindDirection} {WindSpeed}, Осадки: {precipitation}";
        }

    }
}
