import pytest
from origins_env import OriginsEnv  # Assuming your env is in origins_env.py

def test_board_dimensions():
    """Test that the game board has correct dimensions."""
    env = OriginsEnv()
    
    # Test row count
    assert len(env.board) == 8, "Board should have 8 rows"
    
    # Test column count in each row
    for row in env.board:
        assert len(row) == 10, "Each row should have 10 columns"
    
    print("Board dimension test passed successfully!")

if __name__ == "__main__":
    test_board_dimensions()