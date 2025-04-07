from origins_env import OriginsEnv, ELEMENT_POWER

def test_element_hierarchy():
    assert ELEMENT_POWER["Earth"] == "Water", "Earth should beat Water"
    assert ELEMENT_POWER["Air"] == "Earth", "Air should beat Earth"
    print("✓ Element hierarchy test passed")

def test_fire_captures_air():
    env = OriginsEnv()
    env.board[1][1] = "Evolutionist_Air"
    env.move_piece((0, 2), (1, 1))
    assert env.board[1][1] == "Creationist_Fire", "Fire should capture Air"
    print("✓ Fire vs Air capture test passed")

if __name__ == "__main__":
    test_element_hierarchy()
    test_fire_captures_air()
    print("✅ All element tests passed!")