

using UnityEngine;

public class Player : MonoBehaviour
{
	public int previosSide;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (GameManager.Instance.uIManager.gameState == GameState.PLAYING && collision.gameObject.CompareTag("Side") && previosSide != collision.gameObject.GetComponent<Side>().sideIndex)
		{
			previosSide = collision.gameObject.GetComponent<Side>().sideIndex;
			GameManager.Instance.inAir = false;
			if (collision.gameObject.GetComponent<Side>().sideIndex == 0)
			{
				Physics2D.gravity = new Vector2(-9.8f, 0f);
			}
			else
			{
				Physics2D.gravity = new Vector2(9.8f, 0f);
			}
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (GameManager.Instance.uIManager.gameState == GameState.PLAYING && collision.gameObject.CompareTag("Obstacle"))
		{
			if (collision.gameObject.GetComponent<SpriteRenderer>().color == base.gameObject.GetComponent<SpriteRenderer>().color)
			{
				UnityEngine.Object.Destroy(collision.gameObject);
				GameManager.Instance.OpenSides();
				ScoreManager.Instance.UpdateScore(1);
				AudioManager.Instance.PlayEffects(AudioManager.Instance.sameColor);
			}
			else
			{
				AudioManager.Instance.PlayEffects(AudioManager.Instance.wrongColor);
				GameManager.Instance.GameOver();
			}
		}
	}
}
