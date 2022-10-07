// Cpp-LinkedList-Sample.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include <iostream>
using namespace std;

#include "Node.h"



int main()
{
	struct Node* newHead;
	struct Node* head = new Node;

	initNode(head, 10);
	display(head);

	addNode(head, 20);
	display(head);

	addNode(head, 30);
	display(head);

	addNode(head, 35);
	display(head);

	addNode(head, 40);
	display(head);

	addNode(head, 45);
	display(head);
	
	addNode(head, 50);
	display(head);
	
	addNode(head, 55);
	display(head);

	insertFront(&head, 5);
	display(head);
	
	cout << "The list is reversed\n";
	reverse(&head);
	display(head);

	cout << "The list is copied\n";
	copyLinkedList(head, &newHead);
	display(newHead);

	cout << endl;
	int numDel = 5;
	Node* ptrDelete = searchNode(head, numDel);
	if (deleteNode(&head, ptrDelete))
		cout << "OrigList:Node " << numDel << " deleted!\n";
	display(head);

	cout << endl;
	cout << "Comparing the two lists...\n";
	cout << "    original list:  ";
	display(head);
	cout << "    copy of list:   ";
	display(newHead);


	numDel = 5;
	ptrDelete = searchNode(newHead, numDel);
	if (deleteNode(&newHead, ptrDelete)) {
		cout << "NewList: Node " << numDel << " deleted!\n";
		cout << "    New list after delete:      ";
		display(newHead);
	}


	numDel = 35;
	ptrDelete = searchNode(newHead, numDel);
	if (deleteNode(&newHead, ptrDelete)) {
		cout << "NewList: Node " << numDel << " deleted!\n";
		cout << "    New list after delete:      ";
		display(newHead);
	}


	cout << endl;
	cout << "Comparing the two lists...\n";
	cout << "    original list:  ";
	display(head);
	cout << "    copy of list:   ";
	display(newHead);


	cout << endl;
	numDel = 40;
	ptrDelete = searchNode(newHead, numDel);
	if (deleteNode(&newHead, ptrDelete)) {
		cout << "NewList: Node " << numDel << " deleted!\n";
		cout << "    New list after delete:      ";
		display(newHead);
	}

	numDel = 35;
	ptrDelete = searchNode(head, numDel);
	if (deleteNode(&head, ptrDelete)) {
		cout << "OrigList: Node " << numDel << " deleted!\n";
		cout << "    Original list after delete: ";
		display(head);
	}
	numDel = 40;
	ptrDelete = searchNode(head, numDel);
	if (deleteNode(&head, ptrDelete)) {
		cout << "OrigList: Node " << numDel << " deleted!\n";
		cout << "    Original list after delete: ";
		display(head);
	}

	cout << endl;
	cout << "Comparing the two lists again...\n";
	cout << "    original list:  ";
	display(head);
	cout << "    copy of list:   ";
	display(newHead);
	
	cout << "Are the two lists same?\n";
	if (compareLinkedList(head, newHead))
		cout << "Yes, they are same!\n";
	else
		cout << "No, they are different!\n";

	cout << endl;
	cout << "Deleting the copied list\n";
	deleteLinkedList(&newHead);
	display(newHead);
	cout << endl;
	cout << "Deleting the original list\n";
	deleteLinkedList(&head);
	display(head);

	return 0;
}





// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
