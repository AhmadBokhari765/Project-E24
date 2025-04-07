from origins_env import OriginsEnv

def test_observation_shape():
    env = OriginsEnv()
    obs = env.get_observation()
    assert len(obs) == 100, "Observation space should be 100 elements"
    print("✓ Observation shape test passed")

def test_piece_encoding():
    env = OriginsEnv()
    obs = env.get_observation()
    assert obs[0] == 1, "Creationist_Earth should encode as 1"
    assert obs[74] == -5, "Evolutionist_Woman should encode as -5"
    print("✓ Piece encoding test passed")

if __name__ == "__main__":
    test_observation_shape()
    test_piece_encoding()
    print("✅ All observation tests passed!")