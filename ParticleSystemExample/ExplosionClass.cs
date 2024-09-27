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
	public class ExplosionClass : ParticleSystem
	{
		public ExplosionClass(Game game, int maxExplosions) : base(game, maxExplosions * 25) 
		{

		}

		protected override void InitializeConstants()
		{
			textureFilename = "explosion";

			minNumParticles = 20;
			maxNumParticles = 25;

			blendState = BlendState.Additive;
			DrawOrder = AdditiveBlendDrawOrder;
		}

		protected override void InitializeParticle(ref Particle p, Vector2 where)
		{

			var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);

			var lifetime = RandomHelper.NextFloat(0.5f, 1f);

			var acceleration = -velocity / lifetime;

			var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

			var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

			p.Initialize(where, velocity, acceleration, lifetime, rotation, angularVelocity);


		}

		protected override void UpdateParticle(ref Particle particle, float dt)
		{
			base.UpdateParticle(ref particle, dt);

			float normalizeLifetime = particle.TimeSinceStart / particle.Lifetime;

			float alpha = 4 * normalizeLifetime * (1 - normalizeLifetime);

			particle.Color = Color.White * alpha;

			particle.Scale = .5f + .25f * normalizeLifetime;
		}

		public void PlaceExplosion(Vector2 where) => AddParticles(where); 

	}
}
