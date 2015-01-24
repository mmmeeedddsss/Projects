import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;
import java.util.StringTokenizer;


public class WordNet {

		// constructor takes the name of the two input files
		Digraph dg;
		HashMap<String, ArrayList<Integer> > nouns;
		String[] idToNoun;
		HashMap<Integer, String > definitions;
		
		int V = 1;
		
	   public WordNet(String synsets, String hypernyms) //  hypernym (more general synset)
	   {
		   dg = new Digraph(1); //TODO: change value
		   nouns = new HashMap<String, ArrayList<Integer> >();
		   definitions = new HashMap<Integer, String>(); 
		   idToNoun = new String[V];
		   
		   try{
			   Scanner s = new Scanner( new File( synsets ) );
			   Scanner h = new Scanner( new File( hypernyms ) );
			   
			   dg = new Digraph( s.nextInt() ); //TODO: first int must be the number of entitys
			   V = dg.V();
			   idToNoun = new String[V];
			   nouns = new HashMap<String, ArrayList<Integer> >();
			   definitions = new HashMap<Integer, String>(); 
			   
			   while( h.hasNextLine() )
			   {
				   StringTokenizer t = new StringTokenizer(h.nextLine(),",");
				   int syn = Integer.parseInt( t.nextToken() );
				   
				   while( t.hasMoreTokens() )
					   dg.addEdge(syn, Integer.parseInt(t.nextToken(",")));
			   }
			   /*Scanner inp = new Scanner( System.in );
			   Bag<Integer> ii =  (Bag<Integer>) dg.adj( inp.nextInt() );
			   Iterator<Integer> it = ii.iterator();
			   while( it.hasNext() )
				   System.out.println( it.next() );*/
			   s.nextLine();
			   while( s.hasNextLine() )
			   {
				   String l = s.nextLine();
				   
				   //System.out.println(l);
				   
				   int id = Integer.parseInt( l.substring(0, l.indexOf(',')) );
				   StringTokenizer keys = new StringTokenizer(l.substring( l.indexOf(',')+1,l.indexOf(',', l.indexOf(',')+1) )," ");
				   String value = l.substring( l.indexOf(',', l.indexOf(',')+1)+1 );
				   
				   idToNoun[id] = "";
				   //put all nouns to nouns map
				   while( keys.hasMoreTokens() )
				   {
					   String k = keys.nextToken(" ");
					   if( nouns.get(k) == null )
						   nouns.put(k,new ArrayList<Integer>());
					   nouns.get(k).add(id);
					   idToNoun[id] += k + ", "; 
				   }
				   
				   // put def to def map
				   
				   definitions.put(id, value);
				   
				   //System.out.println( id + " - " + keys + " - " + value );
			   }
			   /*for( Iterator<String> ss = nouns.keySet().iterator();ss.hasNext();) // Tester for keys in map nouns
				   System.out.println(ss.next());*/
			   s.close();
			   
			   h.close();
		   }
		   catch( FileNotFoundException e ){  }
	   }

	   //returns all WordNet nouns
	   public Iterable<String> nouns()
	   {
		   return nouns.keySet();
	   }

	   // is the word a WordNet noun?
	   public boolean isNoun(String word)
	   {
		   return nouns.containsKey(word);
	   }

	   /*// distance between nounA and nounB (defined below)
	   public int distance(String nounA, String nounB)

	   // a synset (second field of synsets.txt) that is the common ancestor of nounA and nounB
	   // in a shortest ancestral path (defined below)
	   public String sap(String nounA, String nounB)*/

	   // do unit testing of this class
	   public static void main(String[] args)
	   {
		   Scanner sc = new Scanner( System.in );
		   System.out.println("Give paths of synsets and hypernyms files as .txt");
		   //"/home/mert/Desktop/WordNet/wordnet/synsets.txt"
		   //"/home/mert/Desktop/WordNet/wordnet/hypernyms.txt"
		   //synsets , hypernyms
		   WordNet wn = new WordNet(sc.next(),sc.next());
		   Outcast oc = new Outcast(wn);
		   System.out.println("Database importation has been done");
		   while(true)
		   {
			   System.out.println("for relatedness, press r; for outcast, press o");
			   if( sc.next().contains("r") )
			   {
				   System.out.println("Give 2 nouns");
			   	   oc.findRelatedness( sc.next() , sc.next() );
			   }
			   else
			   {
				   System.out.println("Give n and n nouns");
				   int n = sc.nextInt();
				   String[] nouns = new String[n];
				   for( int i=0; i<n; i++ )
					   nouns[i] = sc.next();
				   System.out.println("Different one is " + oc.outcast(nouns));
			   }
			   System.out.println("\n\nPress t to try again, else program will exit ");
			   if( sc.next().contains("e") )
				   break;
			   for( int i=0; i<50; i++ )
				   System.out.println();
		   }
		   
	   }

}
