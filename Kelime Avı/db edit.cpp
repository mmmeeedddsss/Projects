#include <iostream>
#include <fstream>
#include <string>

using namespace std;

int get_min(int x, int y)
{
	if(x > 0 && y > 0) return (x>y) ? y : x;
	if(x > 0) return x;
	if(y > 0) return y;
	return -1;
}

int main()
{
		ifstream inn;
		ofstream outt;
		inn.open("db.txt");
		outt.open("edited_db.txt");
		
		if (inn.is_open())
		{
			string line;
			string last = "asdasdasbok";
			while ( getline (inn,line) )
			{
					int minn = get_min( line.find(' '),line.find(",") );
					string nline;
					if( minn == -1 )
						nline = line;
					else
						nline = line.substr(0, minn );
					if(nline.length() > 2 && nline != last)
					{
						outt<<nline<<endl;
						last = nline;
					}
			}
			inn.close();
			outt.close();
		}
		return 0;
}
