import java.util.LinkedList;
import java.util.Queue;
import java.util.Scanner;

public class SAP {

	final int MAX_LENGTH = 100;
	
	int distf1[];
	int distf2[];
	
	boolean visited1[];
	
	
	
	Digraph dg;
   // constructor takes a digraph (not necessarily a DAG)
   public SAP(Digraph G)
   {
	   dg = G;
   }

   // length of shortest ancestral path between v and w; -1 if no such path
   public int length(int v, int w)
   {
	   //hMap<Integer, Integer> visitedNodes = new HashMap<Integer, Integer>();
	   //HashMap<Integer, Integer> queues = new HashMap<Integer, Integer>();
	   
	   Queue< Integer > q = new LinkedList<Integer>();
	   
	   distf1 = new int[dg.V()];
	   distf2 = new int[dg.V()];
	   
	   visited1 = new boolean[ dg.V() ];
	   
	   int len = dg.V()+1;
	   
	   q.add(v);
	   visited1[v] = true;
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   for( int nextVertex : dg.adj(currVertex) )
		   {
			   if( visited1[nextVertex] == false )
			   {
				   visited1[nextVertex] = true;
				   distf1[nextVertex] = distf1[currVertex] + 1;
				   q.add(nextVertex);
			   }
		   }
	   }
	   q.clear();
	   
	   q.add(w);
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   
		   //if( len <= distf2[currVertex]+1 )
			   //break;
		   
		   for( int nextVertex : dg.adj(currVertex) )
		   {	   
			   if( distf2[nextVertex] == 0 )
			   {
				   distf2[nextVertex] = distf2[currVertex] + 1;
				   if( visited1[nextVertex] && len > ( distf2[nextVertex] + distf1[nextVertex] ) )
					   len = distf2[nextVertex] + distf1[nextVertex];
				   q.add(nextVertex);
			   }
		   }
	   }
	   
	   
	   
	   return len;
   }

   // a common ancestor of v and w that participates in a shortest ancestral path; -1 if no such path
   public int ancestor(int v, int w)
   {
	   Queue< Integer > q = new LinkedList<Integer>();
	   
	   distf1 = new int[dg.V()];
	   distf2 = new int[dg.V()];
	   
	   visited1 = new boolean[ dg.V() ];
	   
	   int len = dg.V()+1;
	   int anc = -1;
	   
	   q.add(v);
	   visited1[v] = true;
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   for( int nextVertex : dg.adj(currVertex) )
		   {
			   if( visited1[nextVertex] == false )
			   {
				   visited1[nextVertex] = true;
				   distf1[nextVertex] = distf1[currVertex] + 1;
				   q.add(nextVertex);
			   }
		   }
	   }
	   q.clear();
	   
	   q.add(w);
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   
		   //if( len <= distf2[currVertex]+1 )
			   //break;
		   
		   for( int nextVertex : dg.adj(currVertex) )
		   {	   
			   if( distf2[nextVertex] == 0 )
			   {
				   distf2[nextVertex] = distf2[currVertex] + 1;
				   if( visited1[nextVertex] && len > ( distf2[nextVertex] + distf1[nextVertex] ) )
				   {
					   len = distf2[nextVertex] + distf1[nextVertex];
					   anc = nextVertex;
				   }
				   q.add(nextVertex);
			   }
		   }
	   }
	   
	   
	   
	   return anc;
   }

   // length of shortest ancestral path between any vertex in v and any vertex in w; -1 if no such path
   public int length(Iterable<Integer> v, Iterable<Integer> w)
   {
	   Queue< Integer > q = new LinkedList<Integer>();
	   
	   distf1 = new int[dg.V()];
	   distf2 = new int[dg.V()];
	   
	   visited1 = new boolean[ dg.V() ];
	   
	   int len = dg.V()+1;
	   
	   for( int nv : v )
	   {
		   q.add(nv);
		   visited1[nv] = true;
	   }
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   for( int nextVertex : dg.adj(currVertex) )
		   {
			   if( visited1[nextVertex] == false )
			   {
				   visited1[nextVertex] = true;
				   distf1[nextVertex] = distf1[currVertex] + 1;
				   q.add(nextVertex);
			   }
		   }
	   }
	   q.clear();
	   
	   for( int nw : w )
	   {
		   q.add(nw);
		   visited1[nw] = true;
	   }
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   
		   if( len <= distf2[currVertex]+1 )
			   break;
		   
		   for( int nextVertex : dg.adj(currVertex) )
		   {
			   if( distf2[nextVertex] == 0 )
			   {
				   distf2[nextVertex] = distf2[currVertex] + 1;
				   if( visited1[nextVertex] && len > ( distf2[nextVertex] + distf1[nextVertex] ) )
					   len = distf2[nextVertex] + distf1[nextVertex];
				   q.add(nextVertex);
			   }
		   }
	   }
	   
	   
	   
	   return len;
   }

   // a common ancestor that participates in shortest ancestral path; -1 if no such path
   public int ancestor(Iterable<Integer> v, Iterable<Integer> w)
   {
	   Queue< Integer > q = new LinkedList<Integer>();
	   
	   distf1 = new int[dg.V()];
	   distf2 = new int[dg.V()];
	   
	   visited1 = new boolean[ dg.V() ];
	   
	   int len = dg.V()+1;
	   int anc = -1;
	   
	   for( int nv : v )
	   {
		   q.add(nv);
		   visited1[nv] = true;
	   }
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   for( int nextVertex : dg.adj(currVertex) )
		   {
			   if( visited1[nextVertex] == false )
			   {
				   visited1[nextVertex] = true;
				   distf1[nextVertex] = distf1[currVertex] + 1;
				   q.add(nextVertex);
			   }
		   }
	   }
	   q.clear();
	   
	   for( int nw : w )
	   {
		   q.add(nw);
		   visited1[nw] = true;
	   }
	   while( !q.isEmpty() )
	   {
		   int currVertex = q.poll();
		   
		   if( len <= distf2[currVertex]+1 )
			   break;
		   
		   for( int nextVertex : dg.adj(currVertex) )
		   {
			   if( distf2[nextVertex] == 0 )
			   {
				   distf2[nextVertex] = distf2[currVertex] + 1;
				   if( visited1[nextVertex] && len > ( distf2[nextVertex] + distf1[nextVertex] ) )
				   {
					   len = distf2[nextVertex] + distf1[nextVertex];
					   anc = nextVertex;
				   }
				   q.add(nextVertex);
			   }
		   }
	   }
	   
	   
	   
	   return anc;
   }
   
   /*public static void main( String[] args )
   {
	   Scanner s = new Scanner(System.in);
	   Digraph d = new Digraph(s.nextInt());
	   
	   int e = s.nextInt();
	   for( int i=0; i<e; i++ )
		   d.addEdge(s.nextInt(), s.nextInt());
	   
	   SAP sap = new SAP(d);
	   
	   System.out.println(sap.length(s.nextInt(), s.nextInt()));
	   
   }*/

}