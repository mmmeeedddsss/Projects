#include <iostream>
#include <fstream>
#include <vector>
#include <set>
#include <locale>
#include <string>
using namespace std;

const int DEPTH = 10;
const int SIZE = 4;
char **table;
char **used;
set<string> matches;
set<string> db;



int import_db()
{
	ifstream fdb;
	fdb.open("db.txt");
	
	if (fdb.is_open())
	{
		string line;
		while ( getline (fdb,line) )
		{
			//cout<<line<<endl;
			//cin>>line;
			db.insert(line);
			
		}
		fdb.close();
	}
}

int get_input()
{
	table = new char*[SIZE];
	for( int i=0; i<SIZE; i++ )
		table[i] = new char[SIZE];
	
	used = new char*[SIZE];
	for( int i=0; i<SIZE; i++ )
	{
		used[i] = new char[SIZE];
		used[i][0] = '-';
		used[i][1] = '-';
		used[i][2] = '-';
		used[i][3] = '-';
	}
	ifstream in("inp.txt");
	
	for( int i=0; i<SIZE; i++ )
		for( int j=0; j<SIZE; j++ )
			in>>table[j][i];
}

string get_word( unsigned long long w, int len )
{
	//cout<<w<<" - "<<len<<endl;int 
	char *word = new char[len+1];
	for( int i=0; i<len; i++ )
	{
		word[len-i-1] = table[w%10][(w%100)/10];
		w/= 100;
	}
	word[len] = '\0';
	string str = word;
	//cout<<w<<endl;
	return str;
}

bool check_db( string w )
{
	return db.count(w);
}

int maxlen = 0;

void dfs(string w, int x, int y, int d )
{
	if( d == DEPTH )
		return;
	if( used[x][y] == 'x' )
		return;
	used[x][y] = 'x';
	//unsigned long long int new_w =((w*100)+(10*x+y)); // bu node un değeri
	
	string new_w = w + table[x][y];
	
	if( d >= 2 && check_db( new_w /*get_word(new_w,d+1)*/ ))
		matches.insert( new_w /*get_word(new_w,d+1)*/ );
	
	if( x+1<SIZE && y+1<SIZE )dfs( new_w, x+1, y+1, d+1);
	if( x+1<SIZE )dfs( new_w, x+1, y, d+1);
	if( y+1<SIZE )dfs( new_w, x, y+1, d+1);
	if( x-1>=0 && y-1>=0 )dfs( new_w, x-1, y-1, d+1);
	
	if( x-1>=0 && y+1<SIZE )dfs( new_w, x-1, y+1, d+1);
	if( x+1<SIZE && y-1>=0 )dfs( new_w, x+1, y-1, d+1);
	if( x-1>=0 )dfs( new_w, x-1, y, d+1);
	if( y-1>=0 )dfs( new_w, x, y-1, d+1);
	
	used[x][y] = '-';
	return;
}

int process()
{
	for( int y=0; y<SIZE; y++ )
	{
		for( int x=0; x<SIZE; x++ )
		{
			string temp = "";
			dfs(temp,x,y,0);
		}
	}
}

int paste_output()
{
	cout<<endl<<endl;
	
	ofstream out("out.txt");
	
	vector<string> sorted;
	
	for (set<string>::iterator it=matches.begin(); it!=matches.end(); ++it)
    	sorted.push_back(*it);
    
    bool flag;
    for( int i=0; i<sorted.size(); i++ )
    {
  		flag = false;
    	for( int j=i; j<sorted.size(); j++ )
    		if( sorted[i].length() < sorted[j].length() )
   			{
   				string temp = sorted[i];
   				sorted[i] = sorted[j];
   				sorted[j] = temp;
   				flag = true;
		   	}
	   	if( flag == false )
	   		break;
    }
   	for( int i=0; i<sorted.size(); i++ )
   		out<<sorted[i]<<endl;
    
    out.close();
}

int main()
{
	cout<<"Starting to import !"<<endl;
	import_db();
	cout<<"Give input:"<<endl;
	get_input();
	cout<<"Processing ..."<<endl;
	process();
	paste_output();
}
