using System.Configuration;

namespace Hiscraft.WorldModels
{
	/// <summary>
	/// Static class handling and getting world properties.
	/// </summary>
	internal static class WorldConst
	{
		/// <summary>
		/// Size of chunk presented as value x value.
		/// </summary>
		internal static int CHUNK_SIZE;
		/// <summary>
		/// Maximum high of chunk / world.
		/// </summary>
		internal static int HIGH;
		/// <summary>
		/// Water scale value.
		/// </summary>
		internal static int WATER;
		/// <summary>
		/// How many chunk around amera is rendering.
		/// </summary>
		internal static int CHUNK_OFFSET;
		/// <summary>
		/// Texture path corrector.
		/// </summary>
		internal static string texturesPathDEBUG = "../../../Resources/Textures/";
		/// <summary>
		/// Shader path corrector.
		/// </summary>
		internal static string ShadersPathDEBUG = "../../../Resources/Shaders//";
		/// <summary>
		/// Seed passed by user.
		/// </summary>
		internal static int SEED;
		/// <summary>
		/// Bool value if chunks should be generated and renderend after initialize first ones.
		/// </summary>
		internal static bool GENERATE_CHUNK;
		/// <summary>
		/// Texture path corrector.
		/// </summary>
		internal static string TEXTURE_BOOK = "TextureBookUpdate3.png";
		/// <summary>
		/// Scale passed by user for continetalness impact in world shaping.
		/// </summary>
		internal static float CONTINENTALNESS;
		/// <summary>
		/// Scale passed by user for erosions impact in world shaping.
		/// </summary>
		internal static float EROSIONS;
		/// <summary>
		/// Scale passed by user for peak and valleys impact in world shaping.
		/// </summary>
		internal static float PEAKS_AND_VALLEYS;
		/// <summary>
		/// Scale passed by user for three impact in world generating.
		/// </summary>
		internal static float THREES_SCALE;
		/// <summary>
		/// Scale passed by user for special blocks on surface impact in world generating.
		/// </summary>
		internal static float DETAILS_CONGESTION;
		/// <summary>
		/// Scale passed by user for natural resources number undertground in world generating.
		/// </summary>
		internal static int NATURAL_RESOURCES;

		private const float CEP_DIVEDER = 10000f;
		/// <summary>
		/// Methods that read all static generating options form .config file.
		/// </summary>
		/// <exception cref="Exception">When application cant read file correctly so, world should be not procced longer</exception>
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
			THREES_SCALE = t / CEP_DIVEDER;
			DETAILS_CONGESTION = det / CEP_DIVEDER;
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
