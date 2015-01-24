import java.util.ArrayList;


public class Outcast {
	
	WordNet wn;
    public Outcast(WordNet wordnet)         // constructor takes a WordNet object
    {
	   wn = wordnet;
    }
    
    public String outcast(String[] nouns)   // given an array of WordNet nouns, return an outcast
    {
       SAP sap = new SAP(wn.dg);
	   int sumOfDistances[] = new int[nouns.length];
	   for( int i=0; i<nouns.length; i++ )
		   for( int j=0; j<nouns.length; j++ )
		   {
			   String n1 = nouns[i], n2 = nouns[j];
			   
			   if( !wn.isNoun(n1) )  System.out.println( "First noun isn't defined" );
			   if( !wn.isNoun(n2) )  System.out.println( "Second noun isn't defined" );
			   ArrayList<Integer> idN1s = wn.nouns.get(n1);
			   ArrayList<Integer> idN2s = wn.nouns.get(n2);
		    	
		    	//System.out.println(idN1 + " - " + idN2);
		    	
			   sumOfDistances[i] += sap.length(idN1s, idN2s);
		   }
	   int max = 0,index = -1;
	   for( int i=0; i<sumOfDistances.length; i++ )
		   if( max < sumOfDistances[i] )
		   {
			   max = sumOfDistances[i];
			   index = i;
		   }
	   return nouns[index];
    }
    
    public void findRelatedness( String n1, String n2 )
    {
    	if( !wn.isNoun(n1) )  System.out.println( "First noun isn't defined" );
    	if( !wn.isNoun(n2) )  System.out.println( "Second noun isn't defined" );
    	ArrayList<Integer> idN1s = wn.nouns.get(n1);
    	ArrayList<Integer> idN2s = wn.nouns.get(n2);
    	
    	//System.out.println(idN1 + " - " + idN2);
    	
    	SAP sap = new SAP(wn.dg);
    	
    	int len = sap.length(idN1s, idN2s);
    	int anc = sap.ancestor(idN1s, idN2s);
    	if( len > sap.length(idN2s, idN1s))
    	{
    		len = sap.length(idN2s, idN1s);
        	anc = sap.ancestor(idN2s, idN1s);
    	}
    	
    	
    	System.out.println( "With len of "+len+", related Anctestor of them is noun { "+wn.idToNoun[anc].substring(0, wn.idToNoun[anc].lastIndexOf(','))+" }" );
    	System.out.println( "Definitions of this Anctestor is { "+ wn.definitions.get(anc)+" }" );
    }
    //public static void main(String[] args)  // see test client below
}