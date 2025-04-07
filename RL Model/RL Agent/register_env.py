from gym.envs.registration import register

# Register the environment
register(
    id="Origins-v0",  # Unique name for the environment
    entry_point="origins_env:OriginsEnv",  # This should match the module and class name
)
