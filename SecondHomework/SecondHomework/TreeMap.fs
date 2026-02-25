module SecondHomework.TreeMap

type binTree<'value> =
    | Empty
    | Node of 'value * binTree<'value> * binTree<'value>

let rec treeMap f tree cont =
    match tree with
     | Empty -> cont Empty
     | Node (value, leftChild, rightChild) ->
         treeMap f leftChild (fun left ->
             treeMap f rightChild (fun right ->
        
                    let newNode = Node(f value, left, right)
                    cont newNode
                 )
             )