using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ParticleSystemStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleSystem fairyDustRed;
        ParticleSystem fairyDustPink;
        ParticleSystem fireRed;
        ParticleSystem fireOrange;
        ParticleSystem rain;
        Texture2D particleTexture;
        Texture2D pixelTexture;
        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            particleTexture = Content.Load<Texture2D>("particle");
            pixelTexture = Content.Load<Texture2D>("pixel");
            fairyDustRed = new ParticleSystem(GraphicsDevice, 1000, particleTexture);
            //fairyDustRed.Emitter = new Vector2(100, 100);
            fairyDustRed.SpawnPerFrame = 4;
            fairyDustPink = new ParticleSystem(GraphicsDevice, 900, particleTexture);
            fairyDustPink.SpawnPerFrame = 4;
            fireRed = new ParticleSystem(GraphicsDevice, 500, pixelTexture);
            fireRed.SpawnPerFrame = 6;
            fireOrange = new ParticleSystem(GraphicsDevice, 600, pixelTexture);
            fireOrange.SpawnPerFrame = 6;
            rain = new ParticleSystem(GraphicsDevice, 2000, pixelTexture);
            rain.SpawnPerFrame = 10;
            random = new Random();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            rain.Update(gameTime);
            rain.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(500, -300);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-500, 500, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(700, 0, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.3f * new Vector2(0, (float)random.NextDouble());
                particle.Color = Color.LightBlue;
                particle.Scale = 3f;
                particle.Life = 2.0f;
            };
            rain.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            fireRed.Update(gameTime);
            fireRed.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(300, 300);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(30, -30, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(100, 0, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 5f * new Vector2(3, (float)random.NextDouble());
                particle.Color = Color.DarkRed;
                particle.Scale = 3f;
                particle.Life = 2.0f;
            };
            fireRed.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position -= deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            fireOrange.Update(gameTime);
            fireOrange.SpawnParticle = (ref Particle particle) =>
            {
                particle.Position = new Vector2(300, 300);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(30, -30, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(100, 0, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 5f * new Vector2(3, (float)random.NextDouble());
                particle.Color = Color.Orange;
                particle.Scale = 2f;
                particle.Life = 2.0f;
            };
            fireOrange.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position -= deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            fairyDustRed.Update(gameTime);
            fairyDustRed.SpawnParticle = (ref Particle particle) =>
            {
                MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(mouse.X, mouse.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Red;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };

            fairyDustRed.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            fairyDustPink.Update(gameTime);
            fairyDustPink.SpawnParticle = (ref Particle particle) =>
            {
                MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(mouse.X, mouse.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.DeepPink;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };

            fairyDustPink.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            // TODO: Add your drawing code here
            fairyDustRed.Draw();
            fairyDustPink.Draw();
            fireOrange.Draw();
            fireRed.Draw();
            rain.Draw();
            
            base.Draw(gameTime);
        }
    }
}
