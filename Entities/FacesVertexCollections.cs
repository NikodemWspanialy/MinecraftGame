using OpenTK.Mathematics;

namespace Hiscraft.Entities
{
	/// <summary>
	/// Static class that handle all vertex collection (block, half block etc.).
	/// </summary>
	internal static class FacesVertexCollections
	{
		/// <summary>
		/// Vertex collection for simple 1x1x1 block.
		/// </summary>
		internal static readonly Dictionary<FacesEnum, List<Vector3>> BlocksVertexCollection = new()
		{
			{FacesEnum.FRONT, new List<Vector3>()
			{
				new Vector3(-0.5f, 0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, 0.5f, 0.5f), // topright vert
                new Vector3(0.5f, -0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, 0.5f), // bottomleft vert
            } },
			{FacesEnum.BACK, new List<Vector3>()
			{
				new Vector3(0.5f, 0.5f, -0.5f), // topleft vert
                new Vector3(-0.5f, 0.5f, -0.5f), // topright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
            } },
			{FacesEnum.LEFT, new List<Vector3>()
			{
				new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
                new Vector3(-0.5f, 0.5f, 0.5f), // topright vert
                new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            } },
			{FacesEnum.RIGHT, new List<Vector3>()
			{
				new Vector3(0.5f, 0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, 0.5f, -0.5f), // topright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
            } },
			{FacesEnum.TOP, new List<Vector3>()
			{
				new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
                new Vector3(0.5f, 0.5f, -0.5f), // topright vert
                new Vector3(0.5f, 0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, 0.5f, 0.5f), // bottomleft vert
            } },
			{FacesEnum.BOTTOM, new List<Vector3>()
			{
				new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, -0.5f, 0.5f), // topright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            } },
		};

		/// <summary>
		/// Vertex collection for semiblocks.
		/// </summary>
		internal static readonly Dictionary<FacesEnum, List<Vector3>> SemiBlocksVertexCollection = new()
		{
			{FacesEnum.FRONT, new List<Vector3>()
			{
				new Vector3(-0.5f, 0.0f, 0.5f), // topleft vert
                new Vector3(0.5f, 0.0f, 0.5f), // topright vert
                new Vector3(0.5f, -0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, 0.5f), // bottomleft vert
            } },
			{FacesEnum.BACK, new List<Vector3>()
			{
				new Vector3(0.5f, 0.0f, -0.5f), // topleft vert
                new Vector3(-0.5f, 0.0f, -0.5f), // topright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
            } },
			{FacesEnum.LEFT, new List<Vector3>()
			{
				new Vector3(-0.5f, 0.0f, -0.5f), // topleft vert
                new Vector3(-0.5f, 0.0f, 0.5f), // topright vert
                new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            } },
			{FacesEnum.RIGHT, new List<Vector3>()
			{
				new Vector3(0.5f, 0.0f, 0.5f), // topleft vert
                new Vector3(0.5f, 0.0f, -0.5f), // topright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
            } },
			{FacesEnum.TOP, new List<Vector3>()
			{
				new Vector3(-0.5f, 0.0f, -0.5f), // topleft vert
                new Vector3(0.5f, 0.0f, -0.5f), // topright vert
                new Vector3(0.5f, 0.0f, 0.5f), // bottomright vert
                new Vector3(-0.5f, 0.0f, 0.5f), // bottomleft vert
            } },
			{FacesEnum.BOTTOM, new List<Vector3>()
			{
				new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, -0.5f, 0.5f), // topright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            } },
		};

		private static float CactusOffset = 1f / 16f; 

		/// <summary>
		/// Vertex collection for cactus.
		/// </summary>
		internal static readonly Dictionary<FacesEnum, List<Vector3>> CactusVertexCollection = new()
		{
			{FacesEnum.FRONT, new List<Vector3>()
			{
				new Vector3(-0.5f + CactusOffset, 0.5f, 0.5f - CactusOffset), // topleft vert
                new Vector3(0.5f - CactusOffset, 0.5f, 0.5f - CactusOffset), // topright vert
                new Vector3(0.5f - CactusOffset, -0.5f, 0.5f - CactusOffset), // bottomright vert
                new Vector3(-0.5f + CactusOffset, -0.5f, 0.5f - CactusOffset), // bottomleft vert
            } },
			{FacesEnum.BACK, new List<Vector3>()
			{
				new Vector3(0.5f - CactusOffset, 0.5f, -0.5f + CactusOffset), // topleft vert
                new Vector3(-0.5f + CactusOffset, 0.5f, -0.5f + CactusOffset), // topright vert
                new Vector3(-0.5f + CactusOffset, -0.5f, -0.5f + CactusOffset), // bottomright vert
                new Vector3(0.5f - CactusOffset, -0.5f, -0.5f + CactusOffset), // bottomleft vert
            } },
			{FacesEnum.LEFT, new List<Vector3>()
			{
				new Vector3(-0.5f + CactusOffset, 0.5f, -0.5f + CactusOffset), // topleft vert
                new Vector3(-0.5f + CactusOffset, 0.5f, 0.5f - CactusOffset), // topright vert
                new Vector3(-0.5f + CactusOffset, -0.5f, 0.5f - CactusOffset), // bottomright vert
                new Vector3(-0.5f + CactusOffset, -0.5f, -0.5f + CactusOffset), // bottomleft vert
            } },
			{FacesEnum.RIGHT, new List<Vector3>()
			{
				new Vector3(0.5f - CactusOffset, 0.5f, 0.5f - CactusOffset), // topleft vert
                new Vector3(0.5f - CactusOffset, 0.5f, -0.5f + CactusOffset), // topright vert
                new Vector3(0.5f - CactusOffset, -0.5f, -0.5f + CactusOffset), // bottomright vert
                new Vector3(0.5f - CactusOffset, -0.5f, 0.5f - CactusOffset), // bottomleft vert
            } },
			{FacesEnum.TOP, new List<Vector3>()
			{
				new Vector3(-0.5f + CactusOffset, 0.5f, -0.5f + CactusOffset), // topleft vert
                new Vector3(0.5f - CactusOffset, 0.5f, -0.5f + CactusOffset), // topright vert
                new Vector3(0.5f - CactusOffset, 0.5f, 0.5f - CactusOffset), // bottomright vert
                new Vector3(-0.5f + CactusOffset, 0.5f, 0.5f - CactusOffset), // bottomleft vert
            } },
			{FacesEnum.BOTTOM, new List<Vector3>()
			{
				new Vector3(-0.5f + CactusOffset, -0.5f, 0.5f - CactusOffset), // topleft vert
                new Vector3(0.5f - CactusOffset, -0.5f, 0.5f - CactusOffset), // topright vert
                new Vector3(0.5f - CactusOffset, -0.5f, -0.5f + CactusOffset), // bottomright vert
                new Vector3(-0.5f + CactusOffset, -0.5f, -0.5f + CactusOffset), // bottomleft vert
            } },
		};
	}
}
