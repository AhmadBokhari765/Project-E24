import pytest
from origins_env import OriginsEnv

def test_board_dimensions():
    env = OriginsEnv()
    assert len(env.board) == 8, "Should have 8 rows"
    assert all(len(row) == 10 for row in env.board), "Each row should have 10 columns"
    print("✓ Board dimensions test passed - 8x10 grid confirmed")

def test_initial_piece_placement():
    env = OriginsEnv()
    assert env.board[0][0] == "Creationist_Earth", "Top-left should be Creationist Earth"
    assert env.board[7][9] == "Evolutionist_Earth", "Bottom-right should be Evolutionist Earth"
    print("✓ Initial piece placement test passed - Starting positions correct")

def test_starting_positions():
    env = OriginsEnv()
    assert env.creationist_male_pos == (0, 5), "Creationist man at wrong starting position"
    assert env.evolutionist_female_pos == (7, 4), "Evolutionist woman at wrong starting position"
    print("✓ Starting positions test passed - Human pieces placed correctly")

if __name__ == "__main__":
    test_board_dimensions()
    test_initial_piece_placement()
    test_starting_positions()
    print("✅ All board initialization tests passed!")