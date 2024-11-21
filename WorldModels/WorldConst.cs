using System.Configuration;

namespace Hiscraft.WorldModels
{
	internal static class WorldConst
	{
		internal static int CHUNK_SIZE;
		internal static int HIGH;
		internal static int WATER;
		internal static int CHUNK_OFFSET;
		internal static string texturesPathDEBUG = "../../../Resources/Textures/";
		internal static string ShadersPathDEBUG = "../../../Resources/Shaders//";
		internal static int SEED;
		internal static bool GENERATE_CHUNK;
		internal static string TEXTURE_BOOK = "TextureBookUpdate3.png";
		internal static float CONTINENTALNESS;
		internal static float EROSIONS;
		internal static float PEAKS_AND_VALLEYS;
		internal static float THREES_SCALE;
		internal static float DETAILS_CONGESTION;
		internal static int NATURAL_RESOURCES;

		private const float CEP_DIVEDER = 10000f;
		internal static void ReadConst()
		{
			int c = 0, e = 0, p = 0, t = 0, det = 0;
			bool success = true;
			success = success
				 && int.TryParse(ConfigurationManager.AppSettings.Get("chunk_size"), out CHUNK_SIZE)
				 && int.TryParse(ConfigurationManager.AppSettings.Get("high"), out HIGH)
				 && int.TryParse(ConfigurationManager.AppSettings.Get("water_level"), out WATER)
				 && bool.TryParse(ConfigurationManager.AppSettings.Get("render_enable"), out GENERATE_CHUNK)
				 && int.TryParse(ConfigurationManager.AppSettings.Get("render_offset"), out CHUNK_OFFSET)
				 && int.TryParse(ConfigurationManager.AppSettings.Get("seed"), out SEED)
			&& int.TryParse(ConfigurationManager.AppSettings.Get("continentalness"), out c)
			&& int.TryParse(ConfigurationManager.AppSettings.Get("erosions"), out e)
			&& int.TryParse(ConfigurationManager.AppSettings.Get("trees"), out t)
			&& int.TryParse(ConfigurationManager.AppSettings.Get("details_congestion"), out det)
			&& int.TryParse(ConfigurationManager.AppSettings.Get("peaks_and_valleys"), out p)
			&& int.TryParse(ConfigurationManager.AppSettings.Get("natural_resources"), out NATURAL_RESOURCES);

			CONTINENTALNESS = c / CEP_DIVEDER;
			EROSIONS = e / CEP_DIVEDER;
			THREES_SCALE = e / CEP_DIVEDER;
			DETAILS_CONGESTION = e / CEP_DIVEDER;
			PEAKS_AND_VALLEYS = p / CEP_DIVEDER;
			
			success = success
				&& CHUNK_SIZE >= 0
				&& CHUNK_SIZE <= 100
				&& CONTINENTALNESS <= CEP_DIVEDER
				&& CONTINENTALNESS >= 0
				&& EROSIONS <= CEP_DIVEDER
				&& EROSIONS >= 0
				&& PEAKS_AND_VALLEYS <= CEP_DIVEDER
				&& PEAKS_AND_VALLEYS >= 0
				&& DETAILS_CONGESTION <= CEP_DIVEDER
				&& DETAILS_CONGESTION >= 0
				&& THREES_SCALE <= CEP_DIVEDER
				&& THREES_SCALE >= 0
				&& HIGH >= 0
				&& HIGH <= 100
				&& WATER >= 0
				&& WATER <= 10
				&& NATURAL_RESOURCES >= 1
				&& NATURAL_RESOURCES <= 10
				&& CHUNK_OFFSET >= 0
				&& CHUNK_OFFSET <= 10;

			if (!success)
			{
				throw new Exception("Error while reading confg file");
			}
		}
	}
}
