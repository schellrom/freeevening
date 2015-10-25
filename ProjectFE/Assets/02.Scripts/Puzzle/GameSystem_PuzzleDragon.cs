using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary> 
//  3-Matching Game Puzzle
/// </summary>
public class GameSystem_PuzzleDragon : MainGameSystem {
	/////////private variable///////////
	private List<List<Block>> destroyVertical = new List<List<Block>>();
	private List<List<Block>> destroyHorizontal = new List<List<Block>>();

	private List<Block> store = new List<Block>();
	
	private Vector2 _selectedBlock;

	private Vector2 _swapingBlock1;
	private Vector2 _swapingBlock2;
	
	// Use this for initialization
	void Start () {
		// super-class(MainGameSystem) start function called
		base.Start();
	}

	// get tiles position... 
	// how to dispose tiles
	public override Vector2 tilePos(int x,int y){
		if(x >= (int)_tilesNum.x || x < 0)return new Vector2(0,0);
		if(y >= (int)_tilesNum.y || y < 0)return new Vector2(0,0);
		
		Transform trans = _board.transform;
#if(_NGUI_PRO_VERSION_)
		float width = _board.GetComponent<UISprite>().width;
		float height = _board.GetComponent<UISprite>().height;
#else
		float width = trans.localScale.x;
		float height = trans.localScale.y;
#endif
		return new Vector2(
				(x*_tileSize.x) + trans.localPosition.x - width/2 + _tileSize.x/2 + _boardPadding.x + _tilesMargin.x * x,
				((_tilesNum.y-y-1)*_tileSize.y) + trans.localPosition.y - height/2 + _tileSize.y/2 + _boardPadding.y+ _tilesMargin.y * y
			);
	}

	// find 3 or more matching tiles
	int checkDestroyBlock(int type,List<Block> store,List<List<Block>> ori,int i,int j){
		if(_tiles[i,j]._type != type){
			if(store.Count >= 3){
				ori.Add(new List<Block>(store));
			}
			type = _tiles[i,j]._type;
			store.Clear();
		}
		store.Add(_tiles[i,j]);
		return type;
	}

	//find block list block belong to list 
	List<Block> findDestroyBlockInList(List<List<Block>> ori,Block b){
		foreach (List<Block> _list in ori){
			foreach (Block _b in _list){
				if(_b == b)return _list;
			}
		}
		return null;
	}

	//checking 2 list is crossed
	Block findCrossBlock(List<Block> v,List<Block> h){
		foreach (Block _vb in v){
			foreach (Block _hb in h){
				if(_vb == _hb){
					return _vb;
				}
			}
		}
		return null;
	}

	//get tile line
	// first argument is line number
	// second argument is axis. ori == true => x-axis, false => y-axis 
	List<Block> findBlockLine(int line,bool ori){
		List<Block> blocks = new List<Block>();
		if(ori){
			for(int x=0;x < _tilesNum.x; x++){
				blocks.Add(_tiles[x,line]);
			}
		}else{
			for(int y=0;y < _tilesNum.y; y++){
				blocks.Add(_tiles[line,y]);
			}
		}
		return blocks;
	}

	// swap position 2 block
	void swapBlock(int i,int j,int ti,int tj){
		Block tb = _tiles[ti,tj];
		_tiles[ti,tj] = _tiles[i,j];
		_tiles[i,j] = tb;

		_tiles[ti,tj]._posInBoard = new Vector2(ti,tj);
		_tiles[i,j]._posInBoard = new Vector2(i,j);

		_tiles[ti,tj].moveTo(tilePos(ti,tj));
		_tiles[i,j].moveTo(tilePos(i,j));

		_swapingBlock1 = new Vector2(ti,tj);
		_swapingBlock2 = new Vector2(i,j);
	}

	// destroy blocks. and return how many destroyed blocks count.
	// if now monster turn up, player will attacking to monster!
	// if 3 more matching or cross matching, make special tile
	private int destroyBlocks(List<Block> v, Block except=null){
		int count = 0;
		
		foreach (Block _b in v){
			if(except == _b) continue;

			_tiles[(int)_b._posInBoard.x,(int)_b._posInBoard.y] = null;

			GameObject _p = MakeBlockDestroyParticle();
			_p.transform.parent = _panel.transform;
			_p.SendMessage("generate",_b);

			if( _currentMonster != null){
				PlayerAttackParticle __=(MakePlayerAttackParticle()).GetComponent<PlayerAttackParticle>();
				__.generate(_panel,_b,_b.transform.localPosition,new Vector2(0,300));
				__.Finish = OnFinishedPlayerAttack;
			}

			Destroy(_b.gameObject);

			count++;
		}

		return count;
	}

	//==================================
	// update functions start
	//==================================

	// 3 matching process!
	void updateThreeMatching(){
		if( isBlocksMoveToAnim() ) return ;
		if( _gameState == GameState.GAME_SELECT_BLOCK) return ;

		destroyVertical.Clear();
		destroyHorizontal.Clear();
		store.Clear();

		for(int i=0;i < _tilesNum.x ; i++){
			int type=0;
			store.Clear();
			for(int j=0;j < _tilesNum.y ; j++){
				type = checkDestroyBlock(type,store,destroyVertical,i,j);
			}
			if(store.Count >= 3)destroyVertical.Add(new List<Block>(store));
		}
		
		for(int j=0;j < _tilesNum.y ; j++){
			int type=0;
			store.Clear();
			for(int i=0;i < _tilesNum.x ; i++){
				type = checkDestroyBlock(type,store,destroyHorizontal,i,j);
			}
			if(store.Count >= 3)destroyHorizontal.Add(new List<Block>(store));
		}

		List<Block> crossVertical = null;
		List<Block> crossHorizontal = null;
		foreach (List<Block> _list in destroyVertical){
			foreach (Block _b in _list){
				List<Block> ret = findDestroyBlockInList(destroyHorizontal,_b);
				if(ret != null){
					crossVertical = _list;
					crossHorizontal = ret;
					break;
				}
			}
			if(crossVertical != null)break;
		}

		int destroyNum = 0;
		bool anyBlockDestroyed = true;
		if(crossVertical != null){
			Block cross = findCrossBlock(crossVertical,crossHorizontal);
			destroyNum += destroyBlocks(crossVertical  ,cross);
			destroyNum += destroyBlocks(crossHorizontal,cross);

			_tiles[(int)cross._posInBoard.x,(int)cross._posInBoard.y] = null;
			Destroy(cross.gameObject);

			//createSpecialBlock(SpecialBlock.MATCHED_CROSS,cross);
			
		}else if(destroyVertical.Count > 0){
			destroyNum += destroyBlocks(destroyVertical[0],null);
		}else if(destroyHorizontal.Count > 0){
			destroyNum += destroyBlocks(destroyHorizontal[0],null);
		}else{
			anyBlockDestroyed = false;
			if(_gameState == GameState.GAME_ENTRY){
				_gameState = GameState.GAME_WORK;
				_descriptLabel.text = "Go";
#if(_NGUI_PRO_VERSION_)
				_descriptLabel.gameObject.GetComponent<Animator>().Play("ReadyGo_NGUI_Pro");
#else
				_descriptLabel.gameObject.GetComponent<Animator>().Play("ReadyGo");
#endif
			}
		}

		_scoreLabel.addNumberTo(destroyNum*(3*(_currentCombo+1)));

		if(_currentMonster != null){
			if(anyBlockDestroyed){
				increaseCombo();
			}else{
				resetCombo();
			}
		}

		if( _gameState == GameState.GAME_SWAP_BLOCK ){
			if(anyBlockDestroyed == false){
				if(_swapingBlock1.x != -1){
					swapBlock((int)_swapingBlock1.x,(int)_swapingBlock1.y,
							  (int)_swapingBlock2.x,(int)_swapingBlock2.y);
				}
			}else{
				decreaseTurn();
			}
			_gameState = GameState.GAME_PLAYING;
		}
	}

	//update touching or mouse process
	void updateTouchBoard(){
		if(isBlocksMoveToAnim()) return ;

		if (Input.GetMouseButton(0)){ // touch start or mouse clicked

        	Vector3 p = screenTo2DPoint(Input.mousePosition);
			if( intersectNodeToPoint(_board,p) ){
	        	if( _gameState == GameState.GAME_PLAYING ){
	        		Vector2 idx = findIntersectBlock(p);
	        		if(idx.x >= 0){
						_gameState = GameState.GAME_SELECT_BLOCK;
						_selectedBlock = idx;

						Block _b = _tiles[(int)idx.x,(int)idx.y];
						_b.touchDown();
					}
	        	}else if( _gameState == GameState.GAME_SELECT_BLOCK ){
	        		int i = (int)_selectedBlock.x;
	        		int j = (int)_selectedBlock.y;
					
					Vector2 idx = findIntersectBlock(p);
					int ti = (int)idx.x;
					int tj = (int)idx.y;

        			bool isNotBlock = (ti >= 0 && ti < _tilesNum.x && tj >= 0 && tj < _tilesNum.y);

        			_swapingBlock1 = new Vector2(-1,-1);
	        		_swapingBlock2 = new Vector2(-1,-1);
        			if( isNotBlock && (i != ti || j != tj) ){
        				swapBlock(i,j,ti,tj);

        				_selectedBlock = new Vector2(ti,tj);
					}
	        	}
			}

		}else{ // touch end or mouse release

			if(_gameState == GameState.GAME_SELECT_BLOCK){
				Block b = _tiles[(int)_selectedBlock.x,(int)_selectedBlock.y];
				b.touchUp();
	        	_gameState = GameState.GAME_PLAYING;
			}
			
		}
	}

	bool checkIsSameBlock(Block b1,Block b2, Block b3){
		return b1._type == b2._type && b1._type == b3._type;
	}

	//==================================
	// update functions end
	//==================================
	void Update () {
		if(_gameEnd)return ;
		// super-class(MainGameSystem) Update function called
		base.Update();

		updateThreeMatching();
		updateTouchBoard();
	}
}
