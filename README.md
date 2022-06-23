# OffScreenIndicator
unity offscreen indicator

OffScreenIndicator 는 단일 또는 다수의 타겟을 설정하여 화면 안이나 밖에서 GUI로 위치를 나타내 파악 할 수 있도록 해주는 기능 입니다.

<br>

## Preview
![OSI_01](https://user-images.githubusercontent.com/25737436/175325763-877f73e2-2e8e-4ec8-ac3d-2a8c425510bc.gif)
<p align="center">
<b> 3D Sample
</b>
<br>
</p>

![OSI_02](https://user-images.githubusercontent.com/25737436/175326312-812cfc5f-bfff-470b-a8cc-c4fce69e430c.gif)
<p align="center">
<b> 2D Sample
</b>
<br>
</p>
<br>


## Features
1. 타겟을 설정하여 위치를 나타내는 기능
2. 타겟의 범위를 설정해주는 기능 및 미리보기 제공
3. 간단한 사용법 및 테스트 씬 제공


## Supported Version
OffscreenIndicator 는 **Unity 2020.3.32f1** 버전에서 제작 되었습니다. 그러나 다른 버전에서도 작동해야 합니다.


## How to use
1. 하이어라키에 Canvas 생성 후 OffScreenIndicator component 추가 및 Camera 오브젝트 바인딩
![image_guide_01](https://user-images.githubusercontent.com/25737436/175328468-26b553dd-15bf-43ed-abf0-e50677d092b9.PNG)

2. Canvas 하위에 ArrowObjectPool, BoxObjectPool component 추가 및 샘플로 작되어있는 ArrowIndicator Prefab 바인딩
 - 인디케이터 프리팹은 커스터마이징 가능
![image](https://user-images.githubusercontent.com/25737436/175329249-dbc2ac1d-4408-4dcf-b190-988bd0b3c10e.png)

3. 3D 오브젝트 생성 후 Target Component 추가
![image_guide_03](https://user-images.githubusercontent.com/25737436/175329798-b73e22c2-194e-4180-b0db-9accda019789.PNG)

4. 플레이
![image_guide_04](https://user-images.githubusercontent.com/25737436/175330054-efb08dbe-b224-4537-b8cb-0f1c23b98248.PNG)
