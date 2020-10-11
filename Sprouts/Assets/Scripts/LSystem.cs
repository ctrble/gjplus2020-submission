using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// https://www.youtube.com/watch?v=wo3VGyF5z8Q
public class LSystem : MonoBehaviour {
  public int iterations;
  public float angle2d;
  public float angle3d;
  public float width;
  public float minLeafLength;
  public float maxLeafLength;
  public float minBranchLength;
  public float maxBranchLength;
  public float variance;

  public GameObject tempBranchParent;
  public GameObject branchRoot;
  public GameObject branch;
  public GameObject leaf;

  private const string axiom = "X"; // starting point of current path

  private Dictionary<char, string> rules = new Dictionary<char, string>();
  private Stack<SavedTransform> savedTransforms = new Stack<SavedTransform>();
  private Vector3 initialPosition;

  private string currentPath = "";
  private float[] randomRotations;
  public List<string> tempPaths;

  void Awake() {
    randomRotations = new float[1000];
    for (int i = 0; i < randomRotations.Length; i++) {
      randomRotations[i] = Random.Range(-1f, 1f);
    }

    // rules.Add('X', "[*FX][+/FX][+FX][-/-FX]");
    rules.Add('X', "[-/FX][+*FX][++/FX][FX]");
    rules.Add('F', "F");

    currentPath = axiom;
  }

  public void Generate(GameObject tree) {
    // currentPath = axiom;
    StringBuilder stringBuilder = new StringBuilder();

    TreeRoot currentTreeRoot = tree.GetComponent<TreeRoot>();
    int currentGrowthStep = currentTreeRoot.growthStep;

    for (int i = 0; i < iterations; i++) {
      // for (int i = 0; i < currentGrowthStep; i++) {
      char[] currentPathChars = currentPath.ToCharArray();
      for (int j = 0; j < currentPathChars.Length; j++) {
        stringBuilder.Append(rules.ContainsKey(currentPathChars[j]) ? rules[currentPathChars[j]] : currentPathChars[j].ToString());
      }

      currentPath = stringBuilder.ToString();
      stringBuilder = new StringBuilder();
    }

    bool previousWasLeaf = false;
    tempBranchParent = new GameObject("Fresh Branch");

    for (int k = 0; k < currentPath.Length; k++) {
      switch (currentPath[k]) {
        // F == forward
        case 'F':
          initialPosition = transform.position;
          bool isLeaf = false;

          if (initialPosition == Vector3.zero) {
            branchRoot = new GameObject("Root " + k);
            branchRoot.transform.SetParent(tree.transform);
          }

          GameObject currentElement;

          bool nextIsLeaf = currentPath[k + 1] % currentPath.Length == 'X';
          bool aheadIsLeaf = aheadIsLeaf = currentPath[(k + 3) % currentPath.Length] % currentPath.Length == 'F' && currentPath[(k + 4) % currentPath.Length] % currentPath.Length == 'X';

          if (nextIsLeaf || aheadIsLeaf) {
            currentElement = Instantiate(leaf, transform.position, transform.rotation);
            isLeaf = true;
          }
          else {
            currentElement = Instantiate(branch, transform.position, transform.rotation);
          }

          if (previousWasLeaf && !isLeaf) {
            // time to start a new branch
            tempBranchParent = new GameObject("Fresh Branch " + k);
          }
          tempBranchParent.transform.SetParent(branchRoot.transform);

          currentElement.transform.SetParent(tempBranchParent.transform);

          TreeElement currentTreeElement = currentElement.GetComponent<TreeElement>();

          float length = 0f;
          if (isLeaf) {
            length = Random.Range(minLeafLength, maxLeafLength);
            transform.Translate(Vector3.up * 1f * length);
          }
          else {
            length = Random.Range(minBranchLength, maxBranchLength);
            transform.Translate(Vector3.up * 1f * length);
          }

          currentTreeElement.lineRenderer.SetPosition(1, new Vector3(0, length, 0));
          currentTreeElement.lineRenderer.startWidth = currentTreeElement.lineRenderer.startWidth * width;
          currentTreeElement.lineRenderer.endWidth = currentTreeElement.lineRenderer.endWidth * width;
          currentTreeElement.lineRenderer.sharedMaterial = currentTreeElement.material;

          previousWasLeaf = isLeaf;
          break;

        // axiom, nothing happens
        case 'X':
          break;

        case '+':
          transform.Rotate(Vector3.forward * angle2d * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
          break;

        case '-':
          transform.Rotate(Vector3.back * angle2d * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
          break;

        case '*':
          transform.Rotate(Vector3.up * angle3d * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
          break;

        case '/':
          transform.Rotate(Vector3.down * angle3d * (1f + variance / 100f * randomRotations[k % randomRotations.Length]));
          break;

        case '[':
          savedTransforms.Push(new SavedTransform() {
            position = transform.position,
            rotation = transform.rotation
          });
          break;

        case ']':
          SavedTransform stack = savedTransforms.Pop();
          transform.position = stack.position;
          transform.rotation = stack.rotation;
          break;
      }
    }
  }
}
