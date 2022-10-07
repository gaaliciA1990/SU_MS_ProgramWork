#include "Node.h"
#include <cstddef>
#include <iostream>
using namespace std;


// only for the 1st Node
void initNode(struct Node* head, int n) {
	head->data = n;
	head->next = NULL;
}

// appending
void addNode(struct Node* head, int n) {
	Node* newNode = new Node;
	newNode->data = n;
	newNode->next = NULL;

	Node* cur = head;
	while (cur) {
		if (cur->next == NULL) {
			cur->next = newNode;
			return;
		}
		cur = cur->next;
	}
}

void insertFront(struct Node** head, int n) {
	Node* newNode = new Node;
	newNode->data = n;
	newNode->next = *head;
	*head = newNode;
}

struct Node* searchNode(struct Node* head, int n) {
	Node* cur = head;
	while (cur) {
		if (cur->data == n) {
			return cur;
		}
		cur = cur->next;
	}
	cout << "No Node " << n << " in list.\n";
}

bool deleteNode(struct Node** head, Node* ptrDel) {
	Node* cur = *head;
	struct Node* tempNode;

	if (ptrDel == *head) {
		tempNode = *head;
		*head = cur->next;
		delete tempNode;
		return true;
	}

	while (cur) {
		if (cur->next == ptrDel) {
			tempNode = cur->next;
			cur->next = ptrDel->next;	
			delete tempNode;
			return true;
		}
		cur = cur->next;
	}
	return false;
}

/* reverse the list */
struct Node* reverse(struct Node** head)
{
	Node* parent = *head;
	Node* me = parent->next;
	Node* child = me->next;

	/* make parent as tail */
	parent->next = NULL;
	while (child) {
		me->next = parent;
		parent = me;
		me = child;
		child = child->next;
	}
	me->next = parent;
	*head = me;
	return *head;
}

/* Creating a copy of a linked list */
void copyLinkedList(struct Node* node, struct Node** pNew)
{
	if (node != NULL) {
		*pNew = new Node;
		(*pNew)->data = node->data;
		(*pNew)->next = NULL;
		copyLinkedList(node->next, &((*pNew)->next));
	}
}

/* Compare two linked list */
/* return value: same(1), different(0) */
int compareLinkedList(struct Node* node1, struct Node* node2)
{
	static int flag;

	/* both lists are NULL */
	if (node1 == NULL && node2 == NULL) {
		flag = 1;
	}
	else {
		if (node1 == NULL || node2 == NULL)
			flag = 0;
		else if (node1->data != node2->data)
			flag = 0;
		else
			compareLinkedList(node1->next, node2->next);
	}

	return flag;
}

void deleteLinkedList(struct Node** node)
{
	struct Node* tmpNode;
	while (*node) {
		tmpNode = *node;
		*node = tmpNode->next;
		delete tmpNode;
	}
}

void display(struct Node* head) {
	Node* list = head;
	while (list) {
		cout << list->data << " ";
		list = list->next;
	}
	cout << endl;
}


