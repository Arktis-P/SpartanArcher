<div align="center">

# SpartanArcher
스파르타 내일배움캠프에서 Unity 기능을 익히기 위해 궁수의 전설을 레퍼런스로 진행한 팀 프로젝트입니다.

</div>
  
----
  
## 📌 프로젝트 개요
  
- **프로젝트**: Spartan Archer Project  
- **개발환경**: Unity, C#  
- **개발인원**: 4명 (손치완, 서상원, 강기수, 정형권)  
- **타임라인**:  
  🔹25.02.21 (금) 프로젝트 시작, 게임 기획 및 와이어프레임 작성.  
  🔹25.02.27 (목) 프로젝트 v1.0 완성.  
  🔹25.02.28 (금) 프로젝트 v2.0 완성 및 v2.1 빌드.  
- **주요기능**:  
  - 튜토리얼을 통한 게임 조작법 소개 
  - 랜덤 맵 생성
  - 무작위한 숫자의 랜덤 몬스터 생성
  - 스테이지 클리어 시, 스킬 선택 
  - 액티브 스킬 사용
  - 플레이어 및 몬스터 움직임 컨트롤
  - 보스 패턴  

----

## 🧑‍🤝‍🧑 팀원 소개 및 역할 분담

살려주세요! 우리의 코딩을 살려주세요!
| 이름 | 역할 | 상세 내용 |
| ---- | ---- | ---- |
| 손치완 | | |
| 서상원 | | |
| 강기수 | | |
| 정형권 | | |

----
  
## 🎮 게임 설명
  
탑다운 형식의 스테이지 클리어 로그라이크 슈팅 게임으로 2D를 기반으로 만들었습니다.  
화면 비율은 16:9 이며 좌우로 진행하는 방식입니다. 


----

## 🎥 게임 시연 영상


----
  
## 🕹️ 플레이 방법
  
1️⃣ 시작 화면에서 `TUTORIAL` 버튼을 클릭해 기본 조작 방법과 시스템을 익힐 수 있습니다.  
- WASD 혹은 방향키를 사용해 캐릭터를 이동할 수 있습니다.
- 캐릭터의 사정거리 내에 있는 몬스터 중 가장 가까운 거리에 있는 몬스터에게 자동으로 화살이 날아갑니다.
- 체력이 0이 된 몬스터는 처치되어 사라집니다.
- 매 10스테이지 마다 다양한 패턴과 강력한 공격을 하는 보스가 등장합니다.
  
2️⃣ 시작 화면에서 `GAME START` 버튼을 클릭해 게임을 시작할 수 있습니다.
- 매 스테이지 마다 랜덤한 맵이 선택됩니다.
- 매 스테이지 마다 무작위한 수의 랜덤한 몬스터가 스폰되며, 몬스터를 처치하면 점수를 획득할 수 있습니다.
- 매 스테이지를 클리어할 때마다 랜덤하게 등장하는 3가지 스킬 중 하나를 선택할 수 있고, 스킬을 선택한 이후 다음 스테이지로 진행합니다.
- 플레이어의 체력이 0이 된 경우 스테이지 클리어에 실패하게 되며, 재도전을 위해 메인화면으로 돌아갈 수 있습니다.
  
----

## 📖 와이어 프레임
  
Monster와 Boss와 Player의 이동,행동 등은 BaseController를 상속 받아서 사용하도록 했고,  
MonsterManager를 통해서 매 라운드 몬스터를 생성할 수 있도록 만들었습니다.
EntityAnimationHandler를 통해서 겹치게 되는 애니메이션을 함께 관리해주고 있고 Animation을 따로쓰는 Boss는 BossAnimationHandler를 통해서 관리해주고 있습니다.
![GR7](https://github.com/user-attachments/assets/16527ccb-af3c-4b39-96f3-dea6f9ed5b69)

----
  
## 🛠️ 주요 기능
  
#### 1️⃣ 튜토리얼 
튜토리얼 메뉴를 선택하여 튜토리얼 맵으로 진입해서 게임 기본 조작과 시스템을 설명해주도록 구성
![GR1](https://github.com/user-attachments/assets/c4f73c69-dd9a-49af-a1ef-843f353e0f13)

#### 2️⃣ 맵 랜덤 생성
매 라운드 클리어 시 맵 Manager에 등록된 맵 배열에서 랜덤한 맵을 선택하여 생성하도록 구성
![GR5](https://github.com/user-attachments/assets/89bba361-3c88-4892-94df-74b729698f1f)

#### 3️⃣ 몬스터 랜덤 생성 
매 라운드마다 MonsterManager에서 등록된 몬스터 배열에서 랜덤한 몬스터를 선택하여 생성하도록 구성
![GR6](https://github.com/user-attachments/assets/377ae367-b537-479e-a57b-f39a2216251d)

#### 4️⃣ 스킬 선택 로직 
라운드를 클리어시 랜덤한 3개의 효과와 액티브 스킬 중에서 하나를 선택 하여 성장하도록 구성
![GR2](https://github.com/user-attachments/assets/c468ac21-1777-4510-9f67-1cd192460ba5)

#### 5️⃣ 액티브 스킬 사용 
스킬 선택에서 액티브 스킬을 선택 시 화면 UI에 해당하는 스킬의 버튼과 쿨타임을 적용
![GR3](https://github.com/user-attachments/assets/361acd7a-19db-47ff-9c18-4e62f2964963)

#### 6️⃣ 보스 패턴 
10라운드마다 3개의 보스 중 랜덤으로 생성하고 보스는 일정 주기마다 패턴 중 랜덤으로 선택하여 공격
![GR4](https://github.com/user-attachments/assets/0e93244c-e9e3-4c70-ad76-291468ffe892)

----
  
## 🚀 트러블슈팅 (문제해결)
  
#### 1️⃣ Prefab
- 문제: Prefab으로 지정해 게임 중 불러오는 `Map` 인스턴스가 다음 `Map` 인스턴스가 불려온 뒤에도 사라지지 않고 계속해서 남는 문제가 있었다.
- 원인: Prefab 인스턴스를 파괴하는 로직을 추가하지 않았다.
- 해결: 🔵 (1차 시도) 불러올 Prefab을 `lastMap` 변수에 저장함으로써, 다음 Prefab을 불러올 때 `Destroy()` 메서드를 활용해 인스턴스를 지우도록 했다. 🔵 그러나, `lastMap`에 저장된 것은 Prefab 그 자체로, 게임에서 생성된 인스턴스가 아니었기 때문에 파괴되지 않았다.
-  🔵 (2차 시도) Prefab을 불러오는 `Instatiate()` 전체를 `lastMap` 변수에 저장함으로써, 생성된 인스턴스가 파괴될 수 있도록 수정해 해결했다.  
#### 2️⃣ 2차 발사체 문제
- 문제: 발사체 벽 충돌 후 2차 발사체 재생성 시 collision.ClosestPoint를 이용하여 해당 위치에 2차 발사체 재생성 하는 분명 생성했는데 발사체가 사라졌다.
- 원인:  생성되자마자 해당 위치가 collision과 겹쳐있어서 발사체가 collision과 겹친 것 때문이었다.
- 해결: 벽과 충돌했을때 벽면을 구하고 벽의 반대편에 살짝 띄워서 발사체를 생성하기로 변경했다.  
1차 발사체의 위치를 구해서 collision충돌위치를 뺴서 normalized 하여 발사체가 온 방향의 반대방향을 
구한 후에 해당 값으로 벽의 상,하,좌,우를 판단하여 일정값 띄워준 다음 2차 발사체를 재생성했습니다.  
#### 3️⃣ 몬스터 길찾기
- 문제: 몬스터가 장애물 오브젝트와 부딪힌 이후 더 이상 이동하지 못하는 문제가 있었다.
- 원인: 몬스터의 collider와 장애물의 collider가 서로 면 대 면으로 맞닿고, 몬스터의 진행 방향이 충돌한 면과 수직이기 때문이었다.
- 해결: 🔵 (다양한 방안 제시) Unity 3D 프로젝트에서 사용하는 NavMesh 기능을 사용하거나, 몬스터가 일정한 지점을 통과하도록 해 장애물을 피하도록 하는 길찾기 기능을 사용할 수 있다. 🔵 (1차 시도) 몬스터의 collider 크기를 조절하고, 장애물의 collider 모양을 수정함으로써, 몬스터의 진행 방향과 충돌한 면이 수직이 되는 부분을 줄여 문제를 해결하려고 시도했다.  
  🔵 그러나, 여전히 몬스터가 장애물에 ‘비비는’ 현상이 발생했다.  
  🔵 (2차 시도) `Raycast2D` 의 기능을 활용해, 몬스터와 플레이어 사이에 장애물이 있으면 몬스터가 플레이어를 추적하지 않도록 수정함으로써 장애물에 막혀 갇히는 문제를 해결했다.  
#### 4️⃣ GitHub

