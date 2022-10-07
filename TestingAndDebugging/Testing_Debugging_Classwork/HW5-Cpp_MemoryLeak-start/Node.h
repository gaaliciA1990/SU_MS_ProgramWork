#pragma once
struct Node {
	int data;
	Node* next;
};


void initNode(struct Node* head, int n);
void addNode(struct Node* head, int n);
void insertFront(struct Node** head, int n);
struct Node* searchNode(struct Node* head, int n);
bool deleteNode(struct Node** head, Node* ptrDel);
struct Node* reverse(struct Node** head);
void copyLinkedList(struct Node* node, struct Node** pNew);
int compareLinkedList(struct Node* node1, struct Node* node2);
void deleteLinkedList(struct Node** node);
void display(struct Node* head);