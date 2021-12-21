# SKKU-Metaversity

## 프로젝트 배경

- 코로나 19로 인한 온라인 수업 활성화
- 화상회의 플랫폼을 통한 수업의 불편함
  : 학생-교수 간 피드백 어려움, 수업 집중도 저하
- 대학생들이 캠퍼스 생활을 제대로 즐기지 못함

-> 프로젝트를 통해 가상 공간 메타버스를 구축하여 그 안에서 아바타를 활용해 학생과
학생, 교수와 학생 사이의 상호작용을 강화  
-> 실제 수업 분위기를 조성하여 학업 성취도를 높임

## 사용 환경

- 개발 툴
  : Unity, version : 2019.4.29f1 LTS
- 사용 SDK
> Photon PUN2  
Photon Voice

## 백엔드
- Photon Pun2 사용
특징 : P2P 방식, 별도 DB서버 필요, 추후 Photon Server로 네트워크 구조 변환 예정

- PUN 구조
> 최상위 레벨: 네트워크 객체, RPC와 같은 Unity 기능을 구현하는 PUN 코드로 구성.  
 중간 레벨: Photon server와 통신, 매치메이킹, 콜백 함수들과 관련된 로직으로 구성.  
 최하위 레벨: DLL(동적 라이브러리 파일)의 집합으로, 패킷 직렬화/비직렬화 기능 및
프로토콜로 구성

- 서버 호스팅: 세팅법
> 지역별로 여러 서버가 존재하는데, 유니티 상에서 직접 명시해 줄 수 있으며 명시되
지 않았다면, 현재 위치에서 가장 가까운 서버로 연결된다. 이 프로젝트에서는 “kr”
로 명시해 한국 서버로 연결된다.(변경 가능)

(kr 이미지)  

### 서버 설정

모든 설정은 Launcher.cs 스크립트 내에 있다.

- 참여 인원 설정
```
 private byte numPlayers = 4;
```

- 버전 설정 : 버전이 같은 사람들만 매치메이킹 가능
```
 string gameVersion = "1";
```

- 화면 크기 설정
```
Screen.SetResolution(960, 540, false);
```

- room code 생성
```
//nLength로 길이 설정 가능
public static string RandomString(int _nLength = 12)
    {
        const string strPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  //문자 생성 풀
        char[] chRandom = new char[_nLength];

        for (int i = 0; i < _nLength; i++)
        {
            chRandom[i] = strPool[random.Next(strPool.Length)];
        }
        string strRet = new String(chRandom);   // char to string
        return strRet;
 }
 
 
```

### 매치메이킹 방법

PUN은 Room-based matchmaking을 지원함.

- 방 생성방법

(Launcher scene 이미지)  

1. NickName 입력, 캐릭터 선택 후 Create 버튼 클릭 
2. 원하는 Classroom 선택(현재 Basic과 Nature 두 개 있음)
3. ESC를 누르면 메뉴 창 활성화, 방 코드 확인

```
//방 생성 코드
PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
```

- 방 참여 방법

1. NickName 입력, 캐릭터 설정
2. 호스트로부터 받은 방 코드 입력
3. Join Button 클릭


## 인게임 로직

### 기본 사용법

- 오브젝트 상호작용

(상호작용 예시 이미지)

>캐릭터가 상호작용이 가능한 오브젝트를 바라볼 때 오브젝트 하이라이트  
F키 누르면 상호작용 함수 실행

```
//상호작용 구현 상속클래스

abstract public class ObjectInteraction : MonoBehaviour
{
    protected Outline outline;
    private bool isLooked;

    private void Start()
    {
        outline = transform.GetComponent<Outline>();
    }

    private void FixedUpdate()
    {
        isLooked = false;
    }

    private void LateUpdate()
    {
        if (isLooked)
        {
            
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void Highlight()
    {
        isLooked = true;
    }

    abstract public void Interaction();
}
```

- 이모티콘 표현

(이모티콘 예시 이미지)

> T 키를 누르면 이모티콘 선택 가능
원하는 이모티콘 선택 후 B키 누르기

### 강의실 기능

- 파일스캐너 및 PDF 공유

(파일스캐너 이미지)

> 메뉴의 두 번째 버튼인 File버튼을 클릭 시 파일스캐너 활성화  
  현재 폴더의 파일 및 폴더 정보를 보여주며
  PDF는 빨간색, PDF 제외 파일은 흰색, 폴더는 노란색으로 표시  
  모든 파일의 정보 창에서 Open버튼 클릭 시 해당 파일 열 수 있음.  
  PDF의 경우 PDF Viewer로 보내기 버튼을 통해 칠판으로 내보낼 수 있음.


- 비디오 공유

(비디오 공유 이미지)
(인스펙터 창)

>녹화 강의실에서, 녹화한 강의를 틀기 위해 필요한 비디오 플레이어 Prefab으로,  
강의실에서 재생할 동영상을 MP4파일로 저장해 에셋으로 넣은 후 비디오 플레이어의
Video Clip 파라미터에 넣어주면 됨.

(비디오 플레이어 버튼)

>왼쪽부터 재생(노랑), 일시정지(파랑), 초기화(빨강), 음소거(초록) 버튼이며, 버튼 가
까이에서 상호작용 키인 F를 누르면 해당 버튼의 기능이 실행됨.

- 보이스 채팅

> 방에 참여한 모든 사람이 마이크를 통해 대화가 가능함.





 
