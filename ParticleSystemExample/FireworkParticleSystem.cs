using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystemExample
{
	public class  FireworkParticleSystem : ParticleSystem
	{
		Color[] colors = new Color[] { Color.Fuchsia, Color.Red, Color.Crimson, Color.CadetBlue, Color.Aqua, Color.HotPink, Color.LimeGreen };

		Color color;

		public FireworkParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25) 
		{

		}

		protected override void InitializeConstants()
		{
			textureFilename = "circle";

			minNumParticles = 20;
			maxNumParticles = 25;

			blendState = BlendState.Additive;
			DrawOrder = AdditiveBlendDrawOrder;
		}

		protected override void InitializeParticle(ref Particle p, Vector2 where)
		{

			var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 500);

			var lifetime = RandomHelper.NextFloat(0.5f, 1f);

			var acceleration = -velocity / lifetime;

			var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

			var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

			var scale = RandomHelper.NextFloat(4, 6);

			p.Initialize(where, velocity, acceleration, color, lifetime, rotation, angularVelocity, scale);


		}

		protected override void UpdateParticle(ref Particle particle, float dt)
		{
			base.UpdateParticle(ref particle, dt);

			float normalizeLifetime = particle.TimeSinceStart / particle.Lifetime;

			particle.Scale = .5f + .25f * normalizeLifetime;
		}

		public void PlaceFireworks(Vector2 where) 
		{
			color = colors[RandomHelper.Next(0, colors.Length)];
			AddParticles(where);
		}

	}
}
