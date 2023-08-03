using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
/// <summary>
/// �����I�u�W�F�N�g�ɃR���|�[�l���g�AUIDocument�Ƃ��̃X�N���v�g���A�^�b�`����Ă��邱�ƁB
/// ����͉�ʂɕ\������Ă���UI�e�L�X�g�̕\������Ă���e�L�X�g�̎擾�̃T���v��
/// </summary>

public class GamePlayerUI : MonoBehaviour
{
    VisualTreeAsset _elementTemplate;
    UIDocument _document;
    Label _hp, _mp;
    UnityEngine.UIElements.Button _button;
    ProgressBar _progress;

    private void OnEnable()
    {
        if (GetComponent<UIDocument>() != null)//UIDocumen���擾�o�����炻�̂܂܎擾
        {
            _document = GetComponent<UIDocument>();
        }
    }

    private void Start()
    {
        new LabelScoper(_document.rootVisualElement, _elementTemplate, _hp);
        new ButtonChecker(_document.rootVisualElement, _elementTemplate, _button);
    }

    private void Update()
    {
        new ProgressBarController(_document.rootVisualElement, _elementTemplate, _progress);
    }
}

public sealed class LabelScoper//�N���X�錾
{
    private /*readonly*/ VisualTreeAsset _visualTreeAsset;//�����UI�\���̏�񂪊i�[����Ă���̂�readonly�ň��S�ɒl�̎擾
    private /*readonly*/ Label _label;//UIBuilder��Container�ɂ���UI�̗v�f

    public LabelScoper(VisualElement root, VisualTreeAsset visualTreeAsset, Label label)//�N���X�̖{���̊֐��B�I�[�o�[���C�h�̓i�V
    {
        _label = root.Q<Label>("MP");//���[�g����Label�R���|�[�l���g��Name��MP��Label���i�[
        var log = _label.text += " : 100";//�擾����Label�̊i�[���Ă��ĉ�ʕ\������Ă��镶��Label.text���i�[
        Debug.Log(log);
    }
}

public sealed class ButtonChecker
{
    private readonly VisualTreeAsset _visualTreeAsset;
    private readonly UnityEngine.UIElements.Button _button;

    public ButtonChecker(VisualElement root, VisualTreeAsset visualTreeAsset, UnityEngine.UIElements.Button button)//Button�̐錾�������܂��Ȃ���
    {
        _button = root.Q<UnityEngine.UIElements.Button>("TestButton");
        _button.clicked += _button_clicked;
    }

    private void _button_clicked()
    {
        //throw new System.NotImplementedException();
        Debug.Log(_button.text);
    }
}

public sealed class ProgressBarController
{
    private VisualTreeAsset _visualTreeAsset;
    private UnityEngine.UIElements.ProgressBar _progressBar;

    public ProgressBarController(VisualElement root, VisualTreeAsset visualTreeAsset, ProgressBar progressBar)
    {
        _progressBar = root.Q<UnityEngine.UIElements.ProgressBar>("ProgBar");
        _progressBar.value += 1;//�v���O���X�o�[�̒l�̉��Z
    }
}