#include<iostream>
#include<fstream>
#include<thread>
#include<utility>
using namespace std;

char board[9][9];

class pts{
	public:
		int damaTaken;
		int damaGained;
		int pieceTaken;
		int cornerPiece;
		int edgePiece;
		int cornerPieceMoved;
		
		pts()
		{
			damaTaken = 700;
			damaGained = 420;
			pieceTaken = 80;
			cornerPiece = 3;
			edgePiece = 7;
			cornerPieceMoved = 1;
		}	
};

pts *points;

int getPieceVal( char c, int x )
{
	if( ( c == 'W' || c == 'B' ) && ( x == 0 || x == 7 ) )
		return points->damaTaken + points->cornerPiece;
	else if( c == 'W' || c == 'B' )
		return points->damaTaken;
	else if( x == 0 || x == 7 )
		return points->pieceTaken + points->cornerPiece;
	return points->pieceTaken;
}

int getPosVal( int x, int y )
{
	int p = 0;
	if( ( x == 0 && y == 0 ) || ( x == 0 && y == 7 ) || ( x == 7 && y == 0 ) || ( x == 7 && y == 7 ) )
		return p + points->cornerPiece;
	if( x == 0 || x == 7 || y == 0 || x == 0 )
		p += points->edgePiece;
	return p;
}

int move( int x, int y, int newx, int newy, char c )
{
	int p = 0;
	if( x == 0 || x == 7 )
		p += points->cornerPieceMoved;
	if( newy == 0 && c == 'w' )
	{
		board[newx][newy] = 'W';
		p += points->damaGained;
	}
	else if( newy == 7 && c == 'b' )
	{
		board[newx][newy] = 'B';
		p += points->damaGained;
	}
	else 
		board[newx][newy] = c; 
	
	board[x][y] = '*'; 
	
	return p;
	
	//wPos.erase( make_pair(x,y) ); 
	//wPos.insert( make_pair(newx,newy) );
}

int undo( int x, int y, int newx, int newy, char c )
{
	board[x][y] = c; 
	board[newx][newy] = '*'; 
	
	//wPos.erase( make_pair(newx,newy) ); 
	//wPos.insert( make_pair(x,y) );
}

int min( int x, int y )
{
	if( x>y )
		return y;
	return x;
}

int max( int x, int y )
{
	if( x<y )
		return y;
	return x;
}

int evaluate(int);

pair< int,pair<int,int>  > evaluateF( int depth, int x, int y )
{
	if( depth == 0 )
		return make_pair(0,make_pair(0,0));
	
	char t;
	int temp = 0;
	
	pair<int,int> mov;
	if( !( depth % 2 ) ) // playing as white, max
	{
		int maxx = evaluate(depth-1);
		mov = make_pair(x,y);
				if( board[x][y] == 'w' )
				{
								if( x-2>=0 && ( board[x-1][y] == 'b' || board[x-1][y] == 'B' ) && board[x-2][y] == '*' ) 
								{ 
									t=board[x-1][y]; move(x,y,x-2,y,'w'); board[x-1][y] = '*'; 
									pair< int,pair<int,int>  > pr = evaluateF(depth,x-2,y);
									if(maxx < (temp=(pr.first+getPieceVal(t,x-1)+getPosVal(x-2,y)))){ maxx = temp; mov = pr.second; } 
									undo(x,y,x-2,y,'w'); board[x-1][y]=t; 
								}
								if( x+2<=7 && ( board[x+1][y] == 'b' || board[x+1][y] == 'B' ) && board[x+2][y] == '*' ) 
								{ 
									t=board[x+1][y]; move(x,y,x+2,y,'w'); board[x+1][y] = '*'; 
									pair< int,pair<int,int>  > pr = evaluateF(depth,x+2,y);
									if(maxx < (temp=(pr.first+getPieceVal(t,x+1)+getPosVal(x+2,y)))){ maxx = temp; mov = pr.second; } 
									undo(x,y,x+2,y,'w'); board[x+1][y]=t; 
								}
								if( y-2>=0 && ( board[x][y-1] == 'b' || board[x][y-1] == 'B' ) && board[x][y-2] == '*' ) 
								{ 
									t=board[x][y-1]; board[x][y-1] = '*'; 
									int ptss = move(x,y,x,y-2,'w');
									pair< int,pair<int,int>  > pr = evaluateF(depth,x,y-2);
									if(maxx < (temp=(ptss+pr.first+getPieceVal(t,x)+getPosVal(x,y-2)-getPosVal(x,4)))){ maxx = temp; mov = pr.second; } 
									undo(x,y,x,y-2,'w'); board[x][y-1]=t; 
								}
				}
				else if( board[x][y] == 'W' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'W'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(maxx < (temp=(pr.first+getPieceVal(t,x)+getPosVal(x,i)))){ maxx = temp; mov = pr.second; } 
								undo(x,y,x,i,'W'); board[x][pc]=t;
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'W'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(maxx < (temp=(pr.first+getPieceVal(t,x)+getPosVal(x,i)))){ maxx = temp; mov = pr.second; } 
								undo(x,y,x,i,'W'); board[x][pc]=t;
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'W'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(maxx < (temp=(pr.first+getPieceVal(t,i)+getPosVal(i,y)))){ maxx = temp;mov = pr.second; } 
								undo(x,y,i,y,'W'); board[pc][y]=t;
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'W'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(maxx < (temp=(pr.first+getPieceVal(t,i)+getPosVal(i,y)))){ maxx = temp; mov = pr.second; } 
								undo(x,y,i,y,'W'); board[pc][y]=t;
							}
						}
					}
		return make_pair( maxx, mov );
	}
	else
	{
		int minn = evaluate(depth-1);
		mov = make_pair(x,y);
			if( board[x][y] == 'b' )
			{
						if( x-2>=0 && ( board[x-1][y] == 'w' || board[x-1][y] == 'W' ) && board[x-2][y] == '*' ) 
						{ 
							t=board[x-1][y]; move(x,y,x-2,y,'b'); board[x-1][y] = '*';
							pair< int,pair<int,int>  > pr = evaluateF(depth,x-2,y); 
							if(minn > (temp=(pr.first-getPieceVal(t,x-1)-getPosVal(x-2,y)))){ minn = temp; mov = pr.second; } 
							undo(x,y,x-2,y,'b'); board[x-1][y]=t; 
						}
						if( x+2<=7 && ( board[x+1][y] == 'w' || board[x+1][y] == 'W' ) && board[x+2][y] == '*' ) 
						{ 
							t=board[x+1][y]; move(x,y,x+2,y,'b'); board[x+1][y] = '*'; 
							pair< int,pair<int,int>  > pr = evaluateF(depth,x+2,y); 
							if(minn > (temp=(pr.first-getPieceVal(t,x+1)-getPosVal(x+2,y)))){ minn = temp; mov = pr.second; } 
							undo(x,y,x+2,y,'b'); board[x+1][y]=t; 
						}
						if( y+2<=7 && ( board[x][y+1] == 'w' || board[x][y+1] == 'W' ) && board[x][y+2] == '*') 
						{ 
							t=board[x][y+1]; board[x][y+1] = '*'; 
							int ptss = move(x,y,x,y+2,'b');
							pair< int,pair<int,int>  > pr = evaluateF(depth,x,y+2); 
							if(minn > (temp=(-ptss+pr.first-getPieceVal(t,x)-getPosVal(x,y+2)+getPosVal(x,4)))){ minn = temp; mov = pr.second; } 
							undo(x,y,x,y+2,'b'); board[x][y+1]=t; 
						}
			}
			else if( board[x][y] == 'B' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'B'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(minn > (temp=(pr.first-getPieceVal(t,x)-getPosVal(x,i)+getPosVal(x,4)))){ minn = temp; mov = mov = pr.second; } 
								undo(x,y,x,i,'B'); board[x][pc]=t;
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'B'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if( minn > (temp=(pr.first-getPieceVal(t,x)-getPosVal(x,i)))){ minn = temp; mov = mov = pr.second; } 
								undo(x,y,x,i,'B'); board[x][pc]=t;
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'B'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(minn > (temp=(pr.first-getPieceVal(t,i)-getPosVal(i,y)))){ minn = temp; mov = mov = pr.second; } 
								undo(x,y,i,y,'B'); board[pc][y]=t;
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'B'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(minn > (temp=(pr.first-getPieceVal(t,i)-getPosVal(i,y)))){ minn = temp; mov = mov = pr.second; } 
								undo(x,y,i,y,'B'); board[pc][y]=t;
							}
						}
					}
		return make_pair( minn, mov );
	}
}

int evaluate( int depth ) // white trys to maximize , black minimize  
{
	if( depth == 0 )
		return 0;
	
	if( !( depth % 2 ) ) // playing as white, max
	{
		int maxx = -12345;
		
		int temp = 0;
		char t;
		
		bool shouldEat = false;
		
		int x,y;
		for( int y=0; y<8; y++ ) // looking for whether we can take a piece
			for( int x=0; x<8; x++ )
			{
				if( board[x][y] == 'w' )
				{
					if( y-2>=0 && ( board[x][y-1] == 'b' || board[x][y-1] == 'B' ) && board[x][y-2] == '*' ) 
					{
						shouldEat = true;
						goto end_1;
					}
					if( x+2<=7 && ( board[x+1][y] == 'b' || board[x+1][y] == 'B' ) && board[x+2][y] == '*' )
					{
						shouldEat = true;
						goto end_1;
					}
					if( x-2>=0 && ( board[x-1][y] == 'b' || board[x-1][y] == 'B' ) && board[x-2][y] == '*' ) 
					{
						shouldEat = true;
						goto end_1;
					}
				}
				else if( board[x][y] == 'W' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_1;
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_1;
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_1;
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_1;
							}
						}
					}
			}
		end_1:
		//for( set< pair<int,int> >::iterator it = wPos.begin(); it != wPos.end(); it++ ) // picking pieces to try to play
		for( int y=0; y<8; y++ )
			for( int x=0; x<8; x++ )
				{
					//x = (*it).first;
					//y = (*it).second;
					if( board[x][y] == 'w' ) // if using piece is just a normal one
					{
						if( !shouldEat )
						{
							//just play
							if( x-1>=0 && board[x-1][y] == '*' )
								{ move(x,y,x-1,y,'w');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x-1,y)))){ maxx = temp;  } undo(x,y,x-1,y,'w'); }
							if( x+1<=7 && board[x+1][y] == '*' )
								{ move(x,y,x+1,y,'w');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x+1,y)))){ maxx = temp; } undo(x,y,x+1,y,'w'); }
							if( y-1>=0 && board[x][y-1] == '*' )
								{ 
								temp = move(x,y,x,y-1,'w');
								temp += +evaluate(depth-1)+getPosVal(x,y-1)-getPosVal(x,4);
								if(maxx < temp){ maxx = temp;  } undo(x,y,x,y-1,'w'); }
						}
						//take a black piece, as a normal piece, we are taking t
						if( x-2>=0 && ( board[x-1][y] == 'b' || board[x-1][y] == 'B' ) && board[x-2][y] == '*' ) 
						{ 
							t=board[x-1][y]; move(x,y,x-2,y,'w'); board[x-1][y] = '*'; 
							if(maxx < (temp=(evaluateF(depth,x-2,y).first+getPieceVal(t,x-1)+getPosVal(x-2,y)))){ maxx = temp; } 
							undo(x,y,x-2,y,'w'); board[x-1][y]=t; 
						}
						if( x+2<=7 && ( board[x+1][y] == 'b' || board[x+1][y] == 'B' ) && board[x+2][y] == '*' ) 
						{ 
							t=board[x+1][y]; move(x,y,x+2,y,'w'); board[x+1][y] = '*'; 
							if(maxx < (temp=(evaluateF(depth,x+2,y).first+getPieceVal(t,x+1)+getPosVal(x+2,y)))){ maxx = temp; } 
							undo(x,y,x+2,y,'w'); board[x+1][y]=t; 
						}
						if( y-2>=0 && ( board[x][y-1] == 'b' || board[x][y-1] == 'B' ) && board[x][y-2] == '*' ) 
						{ 
							t=board[x][y-1]; board[x][y-1] = '*'; 
							temp = move(x,y,x,y-2,'w');
							temp += +evaluateF(depth,x,y-2).first+getPieceVal(t,x)+getPosVal(x,y-2)-getPosVal(x,4);
							if(maxx < temp){ maxx = temp; } 
							undo(x,y,x,y-2,'w'); board[x][y-1]=t; 
						}
					}
					else if( board[x][y] == 'W' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'W'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(maxx < (temp=(pr.first+getPieceVal(t,x)+getPosVal(x,i)))){ maxx = temp;  } 
								undo(x,y,x,i,'W'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x,i)))){ maxx = temp;  }
								undo(x,y,x,i,'W');
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'W'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(maxx < (temp=(pr.first+getPieceVal(t,x)+getPosVal(x,i)))){ maxx = temp;  } 
								undo(x,y,x,i,'W'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x,i)))){ maxx = temp;  }
								undo(x,y,x,i,'W');
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'W'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(maxx < (temp=(pr.first+getPieceVal(t,i)+getPosVal(i,y)))){ maxx = temp; } 
								undo(x,y,i,y,'W'); board[pc][y]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,i,y,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(i,y)))){ maxx = temp; }
								undo(x,y,i,y,'W');
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'W'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(maxx < (temp=(pr.first+getPieceVal(t,i)+getPosVal(i,y)))){ maxx = temp; } 
								undo(x,y,i,y,'W'); board[pc][y]=t;
							}
							else if( !shouldEat ) // go without eating anything
							{
								move(x,y,i,y,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(i,y)))){ maxx = temp; }
								undo(x,y,i,y,'W');
							}
						}
					}
				}
		return maxx;	
	}
	
	else // playing as black, min
	{
		int minn = 12345;
		
		int temp = 0;
		char t;
		
		bool shouldEat = false;
		
		int x,y;
		for( int y=0; y<8; y++ ) // looking for whether we can take a piece
			for( int x=0; x<8; x++ ) // add B
			{
				if( board[x][y] == 'b' )
				{
					if( x-2>=0 && ( board[x-1][y] == 'w' || board[x-1][y] == 'W' ) && board[x-2][y] == '*' )  
					{
						shouldEat = true;
						break;
					}
					if( x+2<=7 && ( board[x+1][y] == 'w' || board[x+1][y] == 'W' ) && board[x+2][y] == '*' ) 
					{
						shouldEat = true;
						break;
					}
					if( y+2<=7 && ( board[x][y+1] == 'w' || board[x+1][y] == 'W' ) && board[x][y+2] == '*') 
					{
						shouldEat = true;
						break;
					}
				}
				else if( board[x][y] == 'B' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_2;
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_2;
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_2;
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_2;
							}
						}
					}
			}
		end_2:
		//for( set< pair<int,int> >::iterator it = bPos.begin(); it != bPos.end(); it++ ) // picking pieces to try to play
		for( int y=0; y<8; y++ )
			for( int x=0; x<8; x++ )
				if( board[x][y] == 'b' || board[x][y] == 'B' )
				{
			
					//x = (*it).first;
					//y = (*it).second;
					if( board[x][y] == 'b' ) // if using piece is just a normal one
					{
						if( !shouldEat )
						{
							//just play
							if( x-1>=0 && board[x-1][y] == '*' )
								{ move(x,y,x-1,y,'b');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x-1,y)))){ minn = temp; } undo(x,y,x-1,y,'b'); }
							if( x+1<=7 && board[x+1][y] == '*' )
								{ move(x,y,x+1,y,'b');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x+1,y)))){ minn = temp; } undo(x,y,x+1,y,'b'); }
							if( y+1<=7 && board[x][y+1] == '*' )
								{ 
									temp = -move(x,y,x,y+1,'b');
									temp += evaluate(depth-1)-getPosVal(x,y+1)+getPosVal(x,4);
								if(minn > temp){ minn = temp;  } undo(x,y,x,y+1,'b'); }
						}
						//take a white piece, as a normal piece, we are taking t
						if( x-2>=0 && ( board[x-1][y] == 'w' || board[x-1][y] == 'W' ) && board[x-2][y] == '*' ) 
						{ 
							t=board[x-1][y]; move(x,y,x-2,y,'b'); board[x-1][y] = '*'; 
							if(minn > (temp=(evaluateF(depth,x-2,y).first-getPieceVal(t,x-1)-getPosVal(x-2,y)))){ minn = temp;  } 
							undo(x,y,x-2,y,'b'); board[x-1][y]=t; 
						}
						if( x+2<=7 && ( board[x+1][y] == 'w' || board[x+1][y] == 'W' ) && board[x+2][y] == '*' ) 
						{ 
							t=board[x+1][y]; move(x,y,x+2,y,'b'); board[x+1][y] = '*'; 
							if(minn > (temp=(evaluateF(depth,x+2,y).first-getPieceVal(t,x+1)-getPosVal(x+2,y)))){ minn = temp;  } 
							undo(x,y,x+2,y,'b'); board[x+1][y]=t; 
						}
						if( y+2<=7 && ( board[x][y+1] == 'w' || board[x][y+1] == 'W' ) && board[x][y+2] == '*') 
						{ 
							t=board[x][y+1]; board[x][y+1] = '*'; 
							temp = -move(x,y,x,y+2,'b');
							temp += +evaluateF(depth,x,y+2).first-getPieceVal(t,x)-getPosVal(x,y+2)+getPosVal(x,4);
							if(minn > temp){ minn = temp; } 
							undo(x,y,x,y+2,'b'); board[x][y+1]=t; 
						}
					}
					else if( board[x][y] == 'B' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'B'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(minn > (temp=(pr.first-getPieceVal(t,x)-getPosVal(x,i)+getPosVal(x,4)))){ minn = temp; } 
								undo(x,y,x,i,'B'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x,i)))){ minn = temp; }
								undo(x,y,x,i,'B');
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'B'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if( minn > (temp=(pr.first-getPieceVal(t,x)-getPosVal(x,i)))){ minn = temp; } 
								undo(x,y,x,i,'B'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x,i)))){ minn= temp; }
								undo(x,y,x,i,'B');
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'B'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(minn > (temp=(pr.first-getPieceVal(t,i)-getPosVal(i,y)))){ minn = temp; } 
								undo(x,y,i,y,'B'); board[pc][y]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,i,y,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(i,y)))){ minn = temp; }
								undo(x,y,i,y,'B');
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'B'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(minn > (temp=(pr.first-getPieceVal(t,i)-getPosVal(i,y)))){ minn = temp; } 
								undo(x,y,i,y,'B'); board[pc][y]=t;
							}
							else if( !shouldEat ) // go without eating anything
							{
								move(x,y,i,y,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(i,y)))){ minn = temp; }
								undo(x,y,i,y,'B');
							}
						}
					}
				}
		return minn;	
	}

}


int mins[5];
pair< pair<int,int> ,pair<int,int> > movs[5];

int evulateforBlack( int depth, int s, int e, bool shouldEat, int index )
{
	int minn = 12345;
	pair< pair<int,int> ,pair<int,int> > mov = make_pair(make_pair(1,0),make_pair(1,0));
		
	int temp = 0;
	char t;
		
	int x;
	int y;
	for( int i=s; i<e; i++ )
	{
		x = i%8;
		y = i/8;
				if( board[x][y] == 'b' || board[x][y] == 'B' )
				{
					
				
					//x = (*it).first;
					//y = (*it).second;
					if( board[x][y] == 'b' ) // if using piece is just a normal one
					{
						if( !shouldEat )
						{
							//just play
							if( x-1>=0 && board[x-1][y] == '*' )
								{ move(x,y,x-1,y,'b');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x-1,y)))){ minn = temp; mov = make_pair(make_pair(x,y),make_pair(x-1,y)); } 
								undo(x,y,x-1,y,'b'); }
							if( x+1<=7 && board[x+1][y] == '*' )
								{ move(x,y,x+1,y,'b');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x+1,y)))){ minn = temp; mov = make_pair(make_pair(x,y),make_pair(x+1,y)); } 
								undo(x,y,x+1,y,'b'); }
							if( y+1<=7 && board[x][y+1] == '*' )
							{ 
								temp = -move(x,y,x,y+1,'b');
								temp += +evaluate(depth-1)-getPosVal(x,y+1)+getPosVal(x,4);
								if(minn > temp ){ minn = temp; mov = make_pair(make_pair(x,y),make_pair(x,y+1)); } 
								undo(x,y,x,y+1,'b'); 
							}
						}
						//take a white piece, as a normal piece, we are taking t
						if( x-2>=0 && ( board[x-1][y] == 'w' || board[x-1][y] == 'W' ) && board[x-2][y] == '*' ) 
						{ 
							t=board[x-1][y]; move(x,y,x-2,y,'b'); board[x-1][y] = '*'; 
							pair< int,pair<int,int>  > pr = evaluateF(depth,x-2,y);
							if(minn > (temp=(pr.first-getPieceVal(t,x-1)-getPosVal(x-2,y)))){ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
							undo(x,y,x-2,y,'b'); board[x-1][y]=t; 
						}
						if( x+2<=7 && ( board[x+1][y] == 'w' || board[x+1][y] == 'W' ) && board[x+2][y] == '*' ) 
						{ 
							t=board[x+1][y]; move(x,y,x+2,y,'b'); board[x+1][y] = '*'; 
							pair< int,pair<int,int>  > pr = evaluateF(depth,x+2,y);
							if(minn > (temp=(pr.first-getPieceVal(t,x+1)-getPosVal(x+2,y)))){ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
							undo(x,y,x+2,y,'b'); board[x+1][y]=t; 
						}
						if( y+2<=7 && ( board[x][y+1] == 'w' || board[x][y+1] == 'W' ) && board[x][y+2] == '*' ) 
						{ 
							t=board[x][y+1]; board[x][y+1] = '*'; 
							int ptss = move(x,y,x,y+2,'b');
							pair< int,pair<int,int>  > pr = evaluateF(depth,x,y+2);
							if(minn > (temp=(-ptss+pr.first-getPieceVal(t,x)-getPosVal(x,y+2)+getPosVal(x,4))))
								{ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
							undo(x,y,x,y+2,'b'); board[x][y+1]=t; 
						}
					}
					else if( board[x][y] == 'B' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'B'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(minn > (temp=(pr.first-getPieceVal(t,x)-getPosVal(x,i)+getPosVal(x,4)))){ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,x,i,'B'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x,i)))){ minn = temp; mov = make_pair(make_pair(x,y),make_pair(x,i)); }
								undo(x,y,x,i,'B');
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'B'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if( minn > (temp=(pr.first-getPieceVal(t,x)-getPosVal(x,i)))){ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,x,i,'B'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(x,i)))){ minn= temp; mov = make_pair(make_pair(x,y),make_pair(x,i)); }
								undo(x,y,x,i,'B');
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'B'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								temp = pr.first-getPieceVal(t,i)-getPosVal(i,y);
								if(minn > temp){ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,i,y,'B'); board[pc][y]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,i,y,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(i,y)))){ minn = temp; mov = make_pair(make_pair(x,y),make_pair(i,y)); }
								undo(x,y,i,y,'B');
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'B'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(minn > (temp=(pr.first-getPieceVal(t,i)-getPosVal(i,y)))){ minn = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,i,y,'B'); board[pc][y]=t;
							}
							else if( !shouldEat ) // go without eating anything
							{
								move(x,y,i,y,'B');  if(minn > (temp = (evaluate(depth-1)-getPosVal(i,y)))){ minn = temp; mov = make_pair(make_pair(x,y),make_pair(i,y)); }
								undo(x,y,i,y,'B');
							}
						}
					}
					
				}
			}
	mins[index] = minn;
	movs[index] = mov;
	return 0;
}


pair< pair<int,int> ,pair<int,int> > evaluater( int depth ) // white trys to maximize , white minimize  
{
	if( depth == 0 )
		return make_pair(make_pair(0,0),make_pair(0,0));
	
	if( !( depth % 2 ) ) // playing as white, max
	{
		int maxx = -12345;
		pair< pair<int,int> ,pair<int,int> > mov = make_pair(make_pair(0,0),make_pair(0,0));
		
		int temp = 0;
		char t;
		
		bool shouldEat = false;
		
		int x,y;
		for( int y=0; y<8; y++ ) // looking for whether we can take a piece
			for( int x=0; x<8; x++ ) // add W
			{
				if( board[x][y] == 'w' )
				{
					if( y-2>=0 && ( board[x][y-1] == 'b' || board[x][y-1] == 'B' ) && board[x][y-2] == '*' ) 
					{
						shouldEat = true;
						goto end_3;
					}
					if( x+2<=7 && ( board[x+1][y] == 'b' || board[x+1][y] == 'B' ) && board[x+2][y] == '*' )
					{
						shouldEat = true;
						goto end_3;
					}
					if( x-2>=0 && ( board[x-1][y] == 'b' || board[x-1][y] == 'B' ) && board[x-2][y] == '*' ) 
					{
						shouldEat = true;
						goto end_3;
					}
				}
				else if( board[x][y] == 'W' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_3;
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_3;
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_3;
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_3;
							}
						}
					}
			}
		end_3:
		//for( set< pair<int,int> >::iterator it = wPos.begin(); it != wPos.end(); it++ ) // picking pieces to try to play
		for( int y=0; y<8; y++ )
			for( int x=0; x<8; x++ )
			{
				//x = (*it).first;
				//y = (*it).second;
				if( board[x][y] == 'w' ) // if using piece is just a normal one
					{
						if( !shouldEat )
						{
							//just play
							if( x-1>=0 && board[x-1][y] == '*' )
								{ move(x,y,x-1,y,'w');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x-1,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(x-1,y)); } 
								undo(x,y,x-1,y,'w'); }
							if( x+1<=7 && board[x+1][y] == '*' )
								{ move(x,y,x+1,y,'w');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x+1,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(x+1,y)); } 
								undo(x,y,x+1,y,'w'); }
							if( y-1>=0 && board[x][y-1] == '*' )
							{ 
								temp = +move(x,y,x,y-1,'w');
								temp += +evaluate(depth-1)+getPosVal(x,y-1)-getPosVal(x,4);
								if(maxx < temp){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(x,y-1)); } 
								undo(x,y,x,y-1,'w'); 
							}
						}
						//take a black piece, as a normal piece, we are taking t
						if( x-2>=0 && ( board[x-1][y] == 'b' || board[x-1][y] == 'B' ) && board[x-2][y] == '*' ) 
						{ 
							t=board[x-1][y]; move(x,y,x-2,y,'w'); board[x-1][y] = '*'; 
							pair< int,pair<int,int>  > pr = evaluateF(depth,x-2,y);
							if(maxx < (temp=(pr.first+getPieceVal(t,x-1)+getPosVal(x-2,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
							undo(x,y,x-2,y,'w'); board[x-1][y]=t; 
						}
						if( x+2<=7 && ( board[x+1][y] == 'b' || board[x+1][y] == 'B' ) && board[x+2][y] == '*' ) 
						{ 
							t=board[x+1][y]; move(x,y,x+2,y,'w'); board[x+1][y] = '*'; 
							pair< int,pair<int,int>  > pr = evaluateF(depth,x+2,y);
							if(maxx < (temp=(pr.first+getPieceVal(t,x+1)+getPosVal(x+2,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
							undo(x,y,x+2,y,'w'); board[x+1][y]=t; 
						}
						if( y-2>=0 && ( board[x][y-1] == 'b' || board[x][y-1] == 'B' ) && board[x][y-2] == '*' ) 
						{ 
							t=board[x][y-1]; board[x][y-1] = '*'; 
							int ptss = move(x,y,x,y-2,'w');
							pair< int,pair<int,int>  > pr = evaluateF(depth,x,y-2);
							if(maxx < (temp=(ptss+pr.first+getPieceVal(t,x)+getPosVal(x,y-2)-getPosVal(x,4))))
								{ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
							undo(x,y,x,y-2,'w'); board[x][y-1]=t; 
						}
					}
					else if( board[x][y] == 'W' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'W'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(maxx < (temp=(pr.first+getPieceVal(t,x)+getPosVal(x,i)))){ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,x,i,'W'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x,i)))){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(x,i)); }
								undo(x,y,x,i,'W');
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'W' || board[x][i] == 'w' )
								break;
							else if( board[x][i] == 'b' || board[x][i] == 'B' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[x][pc]; move(x,y,x,i,'W'); board[x][pc] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,x,i);
								if(maxx < (temp=(pr.first+getPieceVal(t,x)+getPosVal(x,i)))){ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,x,i,'W'); board[x][pc]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,x,i,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(x,i)))){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(x,i)); }
								undo(x,y,x,i,'W');
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'W'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(maxx < (temp=(pr.first+getPieceVal(t,i)+getPosVal(i,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,i,y,'W'); board[pc][y]=t;
							}
							else if( !shouldEat )// go without eating anything
							{
								move(x,y,i,y,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(i,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(i,y)); }
								undo(x,y,i,y,'W');
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'W' || board[i][y] == 'w' )
								break;
							else if( board[i][y] == 'b' || board[i][y] == 'B' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								t=board[pc][y]; move(x,y,i,y,'W'); board[pc][y] = '*'; 
								pair< int,pair<int,int>  > pr = evaluateF(depth,i,y);
								if(maxx < (temp=(pr.first+getPieceVal(t,i)+getPosVal(i,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),pr.second); } 
								undo(x,y,i,y,'W'); board[pc][y]=t;
							}
							else if( !shouldEat ) // go without eating anything
							{
								move(x,y,i,y,'W');  if(maxx < (temp = (evaluate(depth-1)+getPosVal(i,y)))){ maxx = temp; mov = make_pair(make_pair(x,y),make_pair(i,y)); }
								undo(x,y,i,y,'W');
							}
						}
					}
				}
		cout<<maxx<<endl;
		return mov;	
	}
	
	else // playing as black, min
	{
		int minn = 12345;
		pair< pair<int,int> ,pair<int,int> > mov = make_pair(make_pair(1,0),make_pair(1,0));
		
		int temp = 0;
		char t;
		
		bool shouldEat = false;
		
		int x,y;
		for( int y=0; y<8; y++ ) // looking for whether we can take a piece
			for( int x=0; x<8; x++ ) // add B
			{
				if( board[x][y] == 'b' )
				{
					if( x-2>=0 && ( board[x-1][y] == 'w' || board[x-1][y] == 'W' ) && board[x-2][y] == '*' )  
					{
						shouldEat = true;
						break;
					}
					if( x+2<=7 && ( board[x+1][y] == 'w' || board[x+1][y] == 'W' ) && board[x+2][y] == '*' ) 
					{
						shouldEat = true;
						break;
					}
					if( y+2<=7 && ( board[x][y+1] == 'w' || board[x+1][y] == 'W' ) && board[x][y+2] == '*') 
					{
						shouldEat = true;
						break;
					}
				}
				else if( board[x][y] == 'B' )
					{
						bool flag = false;
						int pc;
						for( int i=y-1;i>=0; i--  ) // to top
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_4;
							}
						}
						
						flag = false;
						for( int i=y+1;i<=7; i++  ) // to bot
						{
							if( board[x][i] == 'B' || board[x][i] == 'b' )
								break;
							else if( board[x][i] == 'W' || board[x][i] == 'w' )
							{
								if( flag == false )
								{
									pc = i;
									flag = true;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_4;
							}
						}
						
						flag = false;
						for( int i=x+1;i<=7; i++  ) // to right
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_4;
							}
						}
						
						flag = false;
						for( int i=x-1;i>=0; i--  ) // to left
						{
							if( board[i][y] == 'B' || board[i][y] == 'b' )
								break;
							else if( board[i][y] == 'W' || board[i][y] == 'w' )
							{
								if( flag == false )
								{
									flag = true;
									pc = i;
								}
								else
									break;
							}
							else if( flag == true )
							{
								shouldEat = true;
								goto end_4;
							}
						}
					}
			}
		end_4:
		
		//for( set< pair<int,int> >::iterator it = bPos.begin(); it != bPos.end(); it++ ) // picking pieces to try to play
		//for( int y=0; y<8; y++ )
			//for( int x=0; x<8; x++ )
			
			/*for( int y1=0; y1<8; y1++, cout<<endl )
			for( int x1=0; x1<8; x1++ )
				cout<<board[x1][y1]<<" ";*/
				
		thread t1(evulateforBlack, depth, 0,16,shouldEat,0);
		thread t2(evulateforBlack, depth, 16,32,shouldEat,0);
		thread t3(evulateforBlack, depth, 32,48,shouldEat,0);
		thread t4(evulateforBlack, depth, 48,64,shouldEat,0);		
		
		t1.join();
		t2.join();
		t3.join();
		t4.join();
			
		for( int i=0; i<4; i++ )
			if( mins[i] < minn )
			{
				minn = mins[i];
				mov = movs[i];
			}	
			
		cout<<minn<<endl;
		return mov;	
	}
}

int main()
{
	ifstream in("board.in");
	ofstream out("move.out");
	
	points = new pts();
	
	int n; 
	cin>>n;
	
	for( int i=0; i<8; i++ )
		for( int j=0; j<8; j++ )
		{
			in>>board[j][i];
		}
	
	pair< pair<int,int> , pair<int,int> > mov = evaluater(n);
	cout<<(mov.first.first+1)<<" - "<<(mov.first.second+1)<<" -->"<<(mov.second.first+1)<<" - "<<(mov.second.second+1)<<endl;
	
	return 0;
}
