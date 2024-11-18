using Hiscraft.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GeneratingTerrain.Collections
{
	internal static class CardinalCollections
	{
		static internal readonly CardinalSplite Continentalness = new CardinalSplite(new List<Point>
			{
				new() {
					X = 0f,
					Y = 1f
				},
				new() {
					X = 0.05f,
					Y = 0f
				},
				new() {
					X = 0.3f,
					Y = 0f
				},
				new() {
					X = 0.35f,
					Y = 0.5f
				},
				new() {
					X = 0.45f,
					Y = 0.5f
				},
				new() {
					X = 0.6f,
					Y = 0.9f
				},
				new() {
					X = 0.7f,
					Y = 0.95f
				},
				new() {
					X = 1f,
					Y = 1f
				},

			});
		static internal readonly CardinalSplite Erosion = new CardinalSplite(new List<Point>
			{
				new() {
					X = 0f,
					Y = 1f
				},
				new() {
					X = 0.05f,
					Y = 0.95f
				},
				new() {
					X = 0.2f,
					Y = 0.8f
				},
				new() {
					X = 0.35f,
					Y = 0.6f
				},
				new() {
					X = 0.40f,
					Y = 0.65f
				},
				new() {
					X = 0.6f,
					Y = 0.2f
				},
				new() {
					X = 0.7f,
					Y = 0.15f
				},
				new() {
					X = 0.8f,
					Y = 0.13f
				},
				new() {
					X = 0.85f,
					Y = 0.5f
				},
				new() {
					X = 0.9f,
					Y = 0.13f
				},
				new() {
					X = 0.9f,
					Y = 0.05f
				},
				new() {
					X = 1f,
					Y = 0.001f
				},

			});
		static internal readonly CardinalSplite Peak = new CardinalSplite(new List<Point>
			{
				new() {
					X = 0f,
					Y = 0f
				},
				new() {
					X = 0.05f,
					Y = 0.07f
				},
				new() {
					X = 0.10f,
					Y = 0.1f
				},
				new() {
					X = 0.20f,
					Y = 0.15f
				},
				new() {
					X = 0.30f,
					Y = 0.17f
				},
				new() {
					X = 0.5f,
					Y = 0.19f
				},
				new() {
					X = 0.6f,
					Y = 0.35f
				},
				new() {
					X = 0.7f,
					Y = 0.55f
				},
				new() {
					X = 0.75f,
					Y = 0.80f
				},
				new() {
					X = 0.8f,
					Y = 0.83f
				},
				new() {
					X = 0.85f,
					Y = 0.85f
				},
				new() {
					X = 0.9f,
					Y = 0.86f
				},
				new() {
					X = 1.0f,
					Y = 0.9f
				},

			});
	}
}
