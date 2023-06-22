using UnityEngine;
using System;

/* ��� */
/*
 * �쐬�FR����
 */

/* �d�l�\�z */
/*
 * �ŏI�I�ɂ܂Ƃ߂Ĉ�̖��O��ԂɏW�񂷂�H��������Ȃ��̂Ō��S�ň��S���̍����R�[�h�ɂ���(1)
 * �ł��邾���V���v���ȏ����ŏI��点��(2)
 */

/* �R�[�h���� */
/* 
 * �e�q�֌W����̑��������N���X
 */

/* �����t���[ */
/*
 * 
 */

/// <summary>
/// �e�q�֌W����̑��������N���X ver - 1.0.0
/// </summary>
public class ObjectsHierarchyController : MonoBehaviour
{
    public GameObject[] _lowerClassObjects;//���w�i�z���j�I�u�W�F�N�g�̊i�[�ϐ�

    /// <summary>
    /// �q�I�u�W�F�N�g�̎擾������GameObject�^�ŕԂ��֐���A�N�e�B�u�ł�OK
    /// </summary>
    /// <param name="parentObject"></param>
    /// <returns></returns>
    public GameObject[] GetChildObjects(GameObject parentObject)
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        GameObject[] _returnObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ�����
        int arrayIndex = 0;//�Y����

        // �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
        foreach (Transform child in parentObject.transform)
        {
            if (child != null)
            {
                _returnObjects[arrayIndex++] = child.gameObject;//GameObject�^�ɕϊ��Ԃ�l�Ɋi�[
            }
            //Debug.Log("child index" + childIndex);
        }
        return _returnObjects;
    }

    /// <summary>
    /// �q�I�u�W�F�N�g���^�O�Ō����������Č��������I�u�W�F�N�g�����ׂĕԂ�
    /// </summary>
    /// <param name="parentObject"></param>
    /// <param name="objectTag"></param>
    /// <returns></returns>
    public GameObject[] GetChildObjectsWithTag(GameObject parentObject, string objectTag)
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        GameObject[] _returnObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ����� step2
        GameObject[] _childObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ����� step1
        int arrayIndex = 0;//�Y����
        int foundObjectCount = 0;//�^�O�����Ńq�b�g�����I�u�W�F�N�g�̑������J�E���g����

        //int arraySizeCount = 0;

        // �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
        foreach (Transform child in parentObject.transform)
        {
            if (child != null)
            {
                _childObjects[arrayIndex++] = child.gameObject;//GameObject�^�ɕϊ��Ԃ�l�Ɋi�[
            }
            //Debug.Log("child index" + childIndex);
        }
        arrayIndex = 0;//������Foreach�Ŏg���Y�����̏������B�����Ȃ����
        //�^�O�̕R�Â��̌���
        foreach (GameObject obj in _childObjects)
        {
            if (obj.gameObject.CompareTag(objectTag))
            {
                _returnObjects[arrayIndex++] = obj;
                foundObjectCount++;
            }
        }
        Array.Resize(ref _returnObjects, foundObjectCount);
        return _returnObjects;
    }

    /// <summary>
    /// �q�I�u�W�F�N�g���^�O�Ō����������Č��������A�N�e�B�u�ɂȂ��Ă�I�u�W�F�N�g�����ׂĕԂ�activeStatus == true��activeSelf�̒l��true�̃I�u�W�F�N�g��T��
    /// </summary>
    /// <param name="parentObject"></param>
    /// <param name="objectTag"></param>
    /// <returns></returns>
    public GameObject[] GetChildObjectsWithTag(GameObject parentObject, string objectTag, bool activeStatus)
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        GameObject[] _returnObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ����� step2
        GameObject[] _childObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ����� step1
        int arrayIndex = 0;//�Y����
        int foundObjectCount = 0;//�^�O�����Ńq�b�g�����I�u�W�F�N�g�̑������J�E���g����

        //int arraySizeCount = 0;

        // �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
        foreach (Transform child in parentObject.transform)
        {
            if (child != null)
            {
                _childObjects[arrayIndex++] = child.gameObject;//GameObject�^�ɕϊ��Ԃ�l�Ɋi�[
            }
            //Debug.Log("child index" + childIndex);
        }
        arrayIndex = 0;//������Foreach�Ŏg���Y�����̏������B�����Ȃ����
        //�^�O�̕R�Â��̌���
        foreach (GameObject obj in _childObjects)
        {
            if (obj.gameObject.CompareTag(objectTag) && obj.activeSelf == activeStatus)
            {
                _returnObjects[arrayIndex++] = obj;
                foundObjectCount++;
            }
        }
        Array.Resize(ref _returnObjects, foundObjectCount);
        return _returnObjects;
    }

    /// <summary>
    /// �q�I�u�W�F�N�g���^�O�Ō����������Č��������I�u�W�F�N�g�z���index=0�̎��̂�Ԃ�
    /// </summary>
    /// <param name="parentObject"></param>
    /// <param name="objectTag"></param>
    /// <returns></returns>
    public GameObject GetRandomlyChildObjectWithTag(GameObject parentObject, string objectTag)
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        GameObject[] _returnObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ����� step2
        GameObject[] _childObjects = new GameObject[parentObject.transform.childCount];//�Y�������ϓ����� step1
        int arrayIndex = 0;//�Y����
        int foundObjectCount = 0;//�^�O�����Ńq�b�g�����I�u�W�F�N�g�̑������J�E���g����

        //int arraySizeCount = 0;

        // �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
        foreach (Transform child in parentObject.transform)
        {
            if (child != null)
            {
                _childObjects[arrayIndex++] = child.gameObject;//GameObject�^�ɕϊ��Ԃ�l�Ɋi�[
            }
            //Debug.Log("child index" + childIndex);
        }
        arrayIndex = 0;//������Foreach�Ŏg���Y�����̏������B�����Ȃ����
        //�^�O�̕R�Â��̌���
        foreach (GameObject obj in _childObjects)
        {
            if (obj.gameObject.CompareTag(objectTag))
            {
                _returnObjects[arrayIndex++] = obj;
                foundObjectCount++;
            }
        }
        Debug.Log("Found Object Is" + foundObjectCount);
        if(foundObjectCount > 0)//�z��̃T�C�Y���Œ�ł�1�m�ۂ���
        {
            Array.Resize(ref _returnObjects, foundObjectCount);
            return _returnObjects[0];//null���Ԃ�Ƃ������郈
        }
        return null;
    }

    /// <summary>
    /// �e�q�֌W��؂�֐� ������GameObject�^
    /// </summary>
    /// <param name="childObject"></param>
    public void MakeChildToParent(GameObject childObject)
    {
        if (childObject != null)//�I�u�W�F�N�g�̒��g�̃`�F�b�N
        {
            childObject.gameObject.transform.parent = null;
        }
    }

    /// <summary>
    /// �e�q�֌W��؂�֐� ������GameObject[]�^
    /// </summary>
    /// <param name="childObjects"></param>
    public void MakeChildToParent(GameObject[] childObjects)
    {
        if (childObjects != null)//�I�u�W�F�N�g�̒��g�̃`�F�b�N
        {
            for (int i = 0; i < childObjects.Length; i++)
            {
                childObjects[i].gameObject.transform.parent = null;
            }
        }
    }

    /// <summary>
    /// �q�I�u�W�F�N�g�ɔC�ӂ̃I�u�W�F�N�g��ς��Ă��܂��֐�
    /// </summary>
    /// <param name="object2Child"></param>
    /// <param name="transform2Child"></param>
    public void MakeParenToChild(GameObject object2Child, Transform transform2Child)
    {
        if (object2Child != null && transform2Child != null)//null-check
        {
            object2Child.gameObject.transform.parent = transform2Child;//�e�q�֌W�R�Â�
            object2Child.transform.localPosition = Vector3.zero;//�ʒu�̏�����
            object2Child.transform.localEulerAngles = Vector3.zero;//��]�̏�����
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�̃A�N�e�B�u��Ԃ̑���
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="activeStatus"></param>
    public void SetChildObjectActiveStatus(GameObject objects, bool activeStatus)
    {
        objects.gameObject.SetActive(activeStatus);
    }

    /// <summary>
    /// �I�u�W�F�N�g�z��̗v�f���ׂẴA�N�e�B�u��Ԃ̑���
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="activeStatus"></param>
    public void SetChildObjectsActiveStatus(GameObject[] objects, bool activeStatus)
    {
        foreach (GameObject obj in objects)
        {
            obj.gameObject.SetActive(activeStatus);
        }
    }
}