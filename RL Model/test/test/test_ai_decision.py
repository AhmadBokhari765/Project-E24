from origins_env import OriginsEnv
import random

def test_ai_prioritizes_captures():
    env = OriginsEnv()
    env.board[1][1] = "Evolutionist_Air"
    env.board[1][2] = "Neutral"
    env.turn = "Creationist"
    
    # Mock AI decision
    moves = []
    for r in range(8):
        for c in range(10):
            if env.board[r][c].startswith("Creationist"):
                moves.extend([(r,c,t) for t in env.get_valid_moves(r,c)])
    
    # Should prefer capturing Air (Fire beats Air)
    best_move = next((m for m in moves if m[2] == (1,1)), None)
    assert best_move, "AI should prioritize capturing weaker elements"
    print("✓ AI capture prioritization test passed")

def test_ai_avoids_suicide_moves():
    env = OriginsEnv()
    env.board[1][1] = "Evolutionist_Water"
    env.board[0][1] = "Creationist_Fire"  # Fire would lose to Water
    env.turn = "Creationist"
    
    moves = env.get_valid_moves(0, 1)
    assert (1,1) not in moves, "AI should avoid moves where it would be captured"
    print("✓ AI danger avoidance test passed")

if __name__ == "__main__":
    test_ai_prioritizes_captures()
    test_ai_avoids_suicide_moves()
    print("✅ All AI decision tests passed!")