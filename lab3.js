const zip= rows=>rows[0].map((_,c)=>rows.map(row=>row[c]))
const COLORS = ["red","green","blue", "orange", "pink", "brown"];
function randint(min, max) {
    var rand = min + Math.random() * (max + 1 - min);
    rand = Math.floor(rand);
    return rand;
}

class GameTree{
	
	constructor(root){
		this.root = root;
	}

	get_leafs(){
		let leafs = [];
		for(let node of this.get_nodes()){
			if (node.is_leaf){
				leafs.push(node);
			}
		}
		return leafs;
	}

	* get_nodes(){
		let node_queue = [this.root];
		while(node_queue.length){
			let node = node_queue.shift();
			yield node;
			for(let child of node.children){
				node_queue.push(child);
			}
		}
	}

	solve_game(){
		let node_queue = this.get_leafs();
		while(node_queue.length){
			let current_node = node_queue.shift();
			if(current_node.gaines.length && current_node.parent){
				node_queue.push(current_node.parent);
				continue;
			}
			if(!current_node.check_children_gaines()){
				node_queue.push(current_node);
				continue;
			}
			current_node.find_optimal_children();
		}
	}

	draw(){
		let graph='digraph G{';
		for(let node of this.get_nodes()){
			let node_label;
			if(node.is_leaf){
				node_label = node.gaines[0].join("\n");
			}
			else{
				node_label = node.player;
			}
			graph += node.name + `[label="${node_label}"];`;
		}
		for(let node of this.get_nodes()){
			for(let child of node.children){
				//graph += `${node.name}->${child.name}`;
				if(!child.is_optimal()){
					graph += `${node.name}->${child.name};`;
					//graph += '[color="red"]';
				}
				//graph += ';';
			}
		}
		let i =-1;
		for(let leaf of this.get_leafs()){
			let node = leaf;
			if(leaf.is_optimal()){
				i++;
			}
			while(node && leaf.is_optimal()){
					if(node.parent){

							console.log(`${node.parent.name}->${node.name}[color="${COLORS[i]}"]`);
							graph += `${node.parent.name}->${node.name}[color="${COLORS[i]}"]`;
					}
					node = node.parent;
			}
		}
		graph += "}";
		let editor = ace.edit("editor");
		editor.setValue(graph);

	}
}

class GameNode{
	constructor(player, parent, is_leaf,layer, name){
		this.player = player
        this.parent = parent;
        this.layer = layer;
        this.children = [];
        this.optimal_children = [];
        this.gaines = [];
        this.is_leaf = is_leaf;
        this.name = name;
	}

	check_children_gaines(){
		for(let child of this.children){
			if(!child.gaines.length){
				return false;
			}
		}
		return true;
	}

	get_children_gaines(){
		let res =[]
		for(let child of this.children){
			for(let gain of child.gaines){
				res.push(gain);
			}
		}
		return zip(res)[this.player-1];
	}

	get_max_gain(){
		if(!this.get_children_gaines()){
			//debugger;
		}
		return Math.max(...this.get_children_gaines());
	}

	find_optimal_children(){
		let max_gain = this.get_max_gain();
		for(let i =0; i< this.children.length; i++){
			for(let j=0; j< this.children[i].gaines.length; j++){
				if(this.children[i].gaines[j][this.player-1] == max_gain){
					if(!this.optimal_children.includes(this.children[i])){
						this.optimal_children.push(this.children[i]);
					}
					this.gaines.push(this.children[i].gaines[j])
				}
			}
		}
	}

	is_optimal(){
		let node = this;
		while(node){
			if(node.parent && !node.parent.optimal_children.includes(node) ){
				return false;
			}
			node = node.parent;
		}
		return true;
	}
}

class TreeGenerator{
	constructor(players_count, max_strat_count, max_gain, min_gain, max_depth, leaf_prob)
	{
		this.players_count = players_count;
		this.max_strat_count = max_strat_count;
		this.max_gain = max_gain;
		this.min_gain = min_gain;
		this.leaf_prob = leaf_prob;
		this.max_depth = max_depth;
	}

	generate_tree(){
		let root = new GameNode(1,null,false,0);
		let node_queue = [root];
		let i =0;
		while(node_queue.length){
			let node = node_queue.shift();
			node.name = 'a' + i;
			this.fill_node(node);
			if(node.layer <= this.max_depth){
				node_queue.push(...node.children);
		    }
			i++;
		}
		return new GameTree(root);
	}

	fill_node(node){
		if(node.parent){
			node.layer = node.parent.layer + 1;
		}
		node.player = (node.layer % this.players_count) + 1;	
		if(Math.random()<this.leaf_prob || node.layer >= this.max_depth){
			node.is_leaf = true;
		} 
		if(node.is_leaf){
			node.gaines.push([]);
			let i = this.players_count;
			while (i>0){
				node.gaines[0].push(randint(this.min_gain, this.max_gain));
				i--;
			}
		} else{
			let strat_count = randint(2, this.max_strat_count);
			let i = strat_count;
			while (i>0){
				node.children.push(new GameNode(0,node));
				i--;
			}	
		}

	}
}

function main(){
	let tree_gen = new TreeGenerator(
		players_count = 3,
		max_strat_count =3,
		max_gain = 10,
		min_gain = -5,
		max_depth = 4,
		leaf_prob = 0.1
	);
	let tree = tree_gen.generate_tree();
	tree.solve_game();
	tree.draw();
}